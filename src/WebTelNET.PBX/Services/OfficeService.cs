using System;
using System.Collections.Generic;
using WebTelNET.CommonNET.Libs;
using WebTelNET.CommonNET.Models;

namespace WebTelNET.PBX.Services
{
    public interface IOfficeService
    {
    }

    public class OfficeService : HttpService, IOfficeService
    {
        public OfficeService() : base("http://localhost:5002/api/")
        {
        }

        public async void GetServiceInfoAsync(string userId, string serviceTypeName)
        {
            var url = "serviceinfo";
            var parameters = new Dictionary<string, string>
            {
                { nameof(userId), userId },
                { nameof(serviceTypeName), serviceTypeName }
            };

            url = $"{url}?{GetQueryString(parameters)}";

            var response = await GetAsync(url);
            var responseModel = ResolveResponseContentAsync<ApiResponseModel>(response.Content);

            Console.WriteLine(response.Content);
        }
    }
}