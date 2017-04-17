using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebTelNET.CommonNET.Services
{
    public abstract class CloudStorageServiceResponse
    {
    }

    public interface ICloudStorageService
    {
        string Token { get; set; }

        /// <summary>
        /// Upload a file from internet by url to storage path
        /// </summary>
        /// <param name="url"></param>
        /// <param name="path"></param>
        Task<CloudStorageServiceResponse> UploadByUrlAsync(string url, string path, bool disableRedirects = true);

//        /// <summary>
//        /// Download file by path
//        /// </summary>
//        /// <param name="path"></param>
//        /// <returns></returns>
        Task<CloudStorageServiceResponse> DownloadByPathAsync(string path);
    }
}