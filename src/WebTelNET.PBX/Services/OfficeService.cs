using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using WebTelNET.CommonNET.Libs;
using WebTelNET.CommonNET.Models;

namespace WebTelNET.PBX.Services
{
    public class ServiceInfoResponseModel
    {
        public DateTime? ActivationDateTime { get; set; }
        public int Status { get; set; }
        public string ServiceType { get; set; }
    }

    public interface IOfficeService
    {
        Task<ServiceInfoResponseModel> GetServiceInfoAsync(string userId, string serviceTypeName);
    }

    public class OfficeService : HttpService, IOfficeService
    {
        public OfficeService() : base("http://localhost:5002/api/")
        {
        }

        public async Task<ServiceInfoResponseModel> GetServiceInfoAsync(string userId, string serviceTypeName)
        {
            var url = "serviceinfo";
            var parameters = new Dictionary<string, string>
            {
                { nameof(userId), userId },
                { nameof(serviceTypeName), serviceTypeName }
            };

            url = $"{url}?{GetQueryString(parameters)}";

            var response = await GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var responseModel = await ResolveResponseContentAsync<ApiResponseModel>(response.Content);
                var model = ((JObject)responseModel.Data.FirstOrDefault().Value).ToObject<ServiceInfoResponseModel>();
                return model;
            }

            throw new Exception(response.Content.ToString());
        }
    }
}