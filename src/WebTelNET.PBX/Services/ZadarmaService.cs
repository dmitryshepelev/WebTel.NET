﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Newtonsoft.Json;

namespace WebTelNET.PBX.Services
{
    /// <summary>
    /// Raise this exception when request is failed to excecute
    /// </summary>
    public class ZadarmaServiceRequestException : Exception
    {
        public HttpStatusCode StatusCode { get; private set; }

        public ZadarmaServiceRequestException(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }

        public ZadarmaServiceRequestException(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }

        public ZadarmaServiceRequestException(HttpStatusCode statusCode, string message, Exception innerException) : base(message, innerException)
        {
            StatusCode = statusCode;
        }
    }

    /// <summary>
    /// Represents Zadarma base response model
    /// </summary>
    public abstract class ZadarmaResponseModel
    {
        /// <summary>
        /// Required property (success or error)
        /// </summary>
        public string Status { get; set; }
    }

    public class ErrorResponseModel : ZadarmaResponseModel
    {
        /// <summary>
        /// Error description
        /// </summary>
        public string Message { get; set; }
    }

    public class BalanceResponseModel : ZadarmaResponseModel
    {
        public float Balance { get; set; }
        public string Currency { get; set; }
    }

    public class PriceInfo
    {
        public string Prefix { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public string Currency { get; set; }
    }

    public class PriceInfoResponseModel : ZadarmaResponseModel
    {
        public PriceInfo Info { get; set; }
    }

    public class ZadarmaService
    {
        private const string BaseAddress = "https://api.zadarma.com";

        public string UserKey { get; }
        public string SecretKey { get; }
        public string ApiVersion { get; private set; }

        public ZadarmaService(string userKey, string secretKey)
        {
            UserKey = userKey;
            SecretKey = secretKey;

            UseV1();
        }

        private string GetMD5(string str)
        {
            using (var hash = MD5.Create())
            {
                var data = hash.ComputeHash(Encoding.UTF8.GetBytes(str));
                var builder = new StringBuilder();
                foreach (byte t in data)
                {
                    builder.Append(t.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private string GetSHA1(string str, string key)
        {
            var strData = Encoding.UTF8.GetBytes(str);
            var keyData = Encoding.UTF8.GetBytes(key);

            using (var sha1 = new HMACSHA1(keyData))
            {
                var hashArray = sha1.ComputeHash(strData);
                return hashArray.Aggregate("", (s, e) => s + string.Format("{0:x2}", e), s => s);
            }
        }

        private string GetRequestUri(string apiMethod)
        {
            return $"/{ApiVersion}/{apiMethod}/";
        }

        private string GetQueryString(IDictionary<string, string> parameters)
        {
            IOrderedEnumerable<KeyValuePair<string, string>> sorted = parameters.OrderBy(x => x.Key);
            return string.Join("$", sorted.Select(x => $"{x.Key}={x.Value}"));
        }

        private async Task<ZadarmaResponseModel> ResolveRequestContentAsync<T>(HttpContent content) where T : ZadarmaResponseModel
        {
            var data = await content.ReadAsStringAsync();
            using (StringReader sr = new StringReader(data))
            {
                JsonTextReader jtr = new JsonTextReader(sr);
                JsonSerializer serializer = new JsonSerializer();
                ZadarmaResponseModel model;
                try
                {
                    model = serializer.Deserialize<T>(jtr);
                }
                catch (Exception)
                {
                    model = serializer.Deserialize<ErrorResponseModel>(jtr);
                }
                finally
                {
                    jtr.Close();
                }
                return model;
            }
        }

        private async Task<HttpResponseMessage> ExecuteRequestAsync(HttpMethod method, string apiMethod, IDictionary<string, string> parameters = null)
        {
            using (var client = new HttpClient())
            {
                if (parameters == null)
                {
                    parameters = new Dictionary<string, string>();
                }
                client.BaseAddress = new Uri(BaseAddress);

                var requestUri = GetRequestUri(apiMethod);
                var queryString = GetQueryString(parameters);
                var str = $"{requestUri}{queryString}{GetMD5(queryString)}";
                var sign = Convert.ToBase64String(Encoding.UTF8.GetBytes(GetSHA1(str, SecretKey)));
                Console.WriteLine(requestUri);
                Console.WriteLine(sign);
                if (!string.IsNullOrEmpty(queryString))
                {
                    requestUri = $"{requestUri}?{queryString}";
                }
                var request = new HttpRequestMessage(method, requestUri);
                request.Headers.TryAddWithoutValidation("Authorization", UserKey + ":" + sign);

                return await client.SendAsync(request);
            }
        }

        /// <summary>
        /// Get current balance of zadarma profile
        /// </summary>
        /// <returns>Something</returns>
        public async Task<ZadarmaResponseModel> GetBalanceAsync()
        {
            var response = await ExecuteRequestAsync(HttpMethod.Get, "info/balance");
            if (response.IsSuccessStatusCode)
            {
                return await ResolveRequestContentAsync<BalanceResponseModel>(response.Content);
            }
            throw new ZadarmaServiceRequestException(response.StatusCode);
        }
        
        /// <summary>
        /// Get call price info to call a certain phone number
        /// </summary>
        /// <param name="number">A phone number to call</param>
        /// <returns></returns>
        public async Task<ZadarmaResponseModel> GetPriceInfoAsync(string number)
        {
            var response = await ExecuteRequestAsync(HttpMethod.Get, "info/price", new Dictionary<string, string> { { nameof(number), number } });
            if (response.IsSuccessStatusCode)
            {
                return await ResolveRequestContentAsync<PriceInfoResponseModel>(response.Content);
            }
            throw new ZadarmaServiceRequestException(response.StatusCode);
        }

        /// <summary>
        /// Force the service to use 'v1' api version
        /// </summary>
        public void UseV1()
        {
            ApiVersion = "v1";
        }
    }
}
