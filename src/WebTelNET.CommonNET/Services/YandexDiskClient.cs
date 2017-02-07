using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WebTelNET.CommonNET.Libs;

namespace WebTelNET.CommonNET.Services
{
    public class ErrorResponse : CloudStorageServiceResponse
    {
        public string Message { get; set; }
        public string Description { get; set; }
        public string Error { get; set; }
    }

    public abstract class FileOperationResponse : CloudStorageServiceResponse
    {
        public string Href { get; set; }
        public HttpMethod Method { get; set; }
        public bool Templated { get; set; }

        protected FileOperationResponse(string href, string method, bool templated)
        {
            Href = href;
            Method = new HttpMethod(method);
            Templated = templated;
        }
    }

    public class FileUploadResponse : FileOperationResponse
    {
        public FileUploadResponse(string href, string method, bool templated) : base(href, method, templated)
        {
        }
    }

    public class FileDownloadResponse : FileOperationResponse
    {
        public FileDownloadResponse(string href, string method, bool templated) : base(href, method, templated)
        {
        }
    }

    public class YandexDiskClient : HttpService, ICloudStorageService
    {
        public string Token { get; set; }

        public YandexDiskClient() : base("https://cloud-api.yandex.net/v1/")
        {
        }

        public YandexDiskClient(string token) : this()
        {
            Token = token;
        }

        private async Task<CloudStorageServiceResponse> _ResolveResponseContentAsync<T>(HttpContent content,
            bool isSuccessStatusCode) where T : CloudStorageServiceResponse
        {
            if (isSuccessStatusCode)
            {
                return await ResolveResponseContentAsync<T>(content);
            }
            return await ResolveResponseContentAsync<ErrorResponse>(content);
        }

        protected override async Task<HttpResponseMessage> ExecuteRequestAsync(HttpRequestMessage request)
        {
            if (string.IsNullOrEmpty(Token)) throw new NullReferenceException("Token must be set");
            request.Headers.TryAddWithoutValidation("Authorization", "OAuth " + Token);
            return await base.ExecuteRequestAsync(request);
        }

        public async Task<CloudStorageServiceResponse> UploadByUrlAsync(string url, string path, bool disableRedirects = true)
        {
            const string apiUrl = "disk/resources/upload";
            var parameters = new Dictionary<string, string>
            {
                { nameof(url), url },
                { nameof(path), path },
                { "disable_redirects", disableRedirects.ToString() }
            };
            var response = await PostAsync($"{apiUrl}?{GetQueryString(parameters)}", null);
            return await _ResolveResponseContentAsync<FileUploadResponse>(response.Content, response.IsSuccessStatusCode);
        }

        public async Task<CloudStorageServiceResponse> DownloadByPathAsync(string path)
        {
            const string apiUrl = "disk/resources/download";
            var parameters = new Dictionary<string, string>
            {
                { nameof(path), path }
            };
            var response = await GetAsync($"{apiUrl}?{GetQueryString(parameters)}");
            return await _ResolveResponseContentAsync<FileDownloadResponse>(response.Content, response.IsSuccessStatusCode);
        }
    }
}