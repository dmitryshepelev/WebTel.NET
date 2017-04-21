using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using WebTelNET.CommonNET.Libs;
using WebTelNET.CommonNET.Models;
using WebTelNET.Office.Libs.Models;

namespace WebTelNET.Office.Libs.Services
{
    public interface IOfficeClient
    {
        Task<ServiceInfoResponseModel> GetServiceInfoAsync(string userId, string serviceTypeName);
        Task<ServiceDataResponseModel> GetServiceDataAsync(string userId, string serviceTypeName, string dataKey);
    }

    public class OfficeClient : HttpService, IOfficeClient
    {
        private IOfficeClient _officeClientImplementation;

        public OfficeClient() : base("http://localhost:5002/api/")
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

        public async Task<ServiceDataResponseModel> GetServiceDataAsync(string userId, string serviceTypeName, string dataKey)
        {
            var url = "servicedata";
            var parameters = new Dictionary<string, string>
            {
                { nameof(userId), userId },
                { nameof(serviceTypeName), serviceTypeName },
                { nameof(dataKey), dataKey }
            };

            url = $"{url}?{GetQueryString(parameters)}";

            var response = await GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var responseModel = await ResolveResponseContentAsync<ApiResponseModel>(response.Content);
                var model = new ServiceDataResponseModel();
                model.Data = (string)responseModel.Data.FirstOrDefault().Value;
                // var model = ((JObject)responseModel.Data.FirstOrDefault()).ToObject<ServiceDataResponseModel>();
                return model;
            }

            throw new Exception(response.Content.ToString());
        }
    }
}