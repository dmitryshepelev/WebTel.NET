using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace WebTelNET.CommonNET.Libs
{
    public interface ICrudExecutor
    {
        Task<HttpResponseMessage> PostAsync(string url, object data);
        Task<HttpResponseMessage> GetAsync(string url);
    }

    public interface IQueryStringFormatter
    {
        /// <summary>
        /// Get query string from IDictionary
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        string GetQueryString(IDictionary<string, string> parameters);
    }

    /// <summary>
    /// Abstarct class to provide HttpService
    /// </summary>
    /// <typeparam name="T">ResponseMessage type</typeparam>
    public abstract class HttpService : ICrudExecutor, IQueryStringFormatter
    {
        public string BaseAddress { get; }

        protected HttpService(string baseAddress)
        {
            BaseAddress = baseAddress;
        }

        /// <summary>
        /// Creates a Response model of type T
        /// </summary>
        /// <param name="content">Response content</param>
        /// <typeparam name="T">Type of the model to fill the successfully completed request</typeparam>
        /// <returns>Instance of T</returns>
        protected async Task<T> ResolveResponseContentAsync<T>(HttpContent content)
            where T : class
        {
            var data = await content.ReadAsStringAsync();
            using (var sr = new StringReader(data))
            {
                var jtr = new JsonTextReader(sr);
                var serializer = new JsonSerializer();
                var model = serializer.Deserialize<T>(jtr);
                jtr.Close();
                return model;
            }
        }

        /// <summary>
        /// Executes HttpRequestMessage
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected virtual async Task<HttpResponseMessage> ExecuteRequestAsync(HttpRequestMessage request)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseAddress);
                return await client.SendAsync(request);
            }
        }

        /// <summary>
        /// Creates query string from the dictionary
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public virtual string GetQueryString(IDictionary<string, string> parameters)
        {
            return string.Join("&", parameters.Select(x => $"{x.Key}={WebUtility.UrlEncode(x.Value)}"));
        }

        /// <summary>
        /// Execute POST request
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public virtual async Task<HttpResponseMessage> PostAsync(string url, object data)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);

            if (data != null)
            {
                throw new NotImplementedException();
            }

            return await ExecuteRequestAsync(request);
        }

        /// <summary>
        /// Execute GET request
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public virtual async Task<HttpResponseMessage> GetAsync(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            return await ExecuteRequestAsync(request);
        }
    }
}