using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
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

    public struct ZadarmaResponseStatus
    {
        public static string Success => "success";
        public static string Error => "error";
    }

    public enum ResponseVersion
    {
        Old = 1,
        New = 2
    }

    public enum BoundDateTimeKind
    {
        Start,
        End
    }

    #region Abstractions

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

    /// <summary>
    /// Statistics info base model
    /// </summary>
    public abstract class StatisticsInfo
    {
        public string Sip { get; set; }
        public DateTime CallStart { get; set; }
        public string Disposition { get; set; }
    }

    /// <summary>
    /// Statistics response base model
    /// </summary>
    public abstract class StatisticsResponseModel : ZadarmaResponseModel
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }

    /// <summary>
    /// Provide to class working with Currency property
    /// </summary>
    public interface ICurrency
    {
        string Currency { get; set; }
    }

    #endregion

    #region Response Models

    /// <summary>
    /// Response model for failure api responses
    /// </summary>
    public class ErrorResponseModel : ZadarmaResponseModel
    {
        /// <summary>
        /// Error description
        /// </summary>
        public string Message { get; set; }
    }

    /// <summary>
    /// Response model for /info/balance/ api method
    /// </summary>
    public class BalanceResponseModel : ZadarmaResponseModel, ICurrency
    {
        public float Balance { get; set; }
        public string Currency { get; set; }
    }

    /// <summary>
    /// Response model for /info/price/ api method
    /// </summary>
    public class PriceInfoResponseModel : ZadarmaResponseModel
    {
        public PriceInfo Info { get; set; }
    }

    /// <summary>
    /// Response model for /statistics/ api method
    /// </summary>
    public class OverallStatisticsResponseModel : StatisticsResponseModel
    {
        public IList<OverallStatisticsInfo> Stats { get; set; }
    }

    /// <summary>
    /// Response model for /statistics/pbx/ api method
    /// </summary>
    public class PBXStatisticsResponseModel : StatisticsResponseModel
    {
        public ResponseVersion Version { get; set; }
        public IList<PBXStatisticsInfo> Stats { get; set; }

        public IEnumerable<IGrouping<string, PBXStatisticsInfo>> Group()
        {
            if (Version == ResponseVersion.New)
            {
                return Stats.GroupBy(x => x.pbx_call_id);
            }
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Response model from /reqeust/callback/ api method
    /// </summary>
    public class RequestCallbackResponseModel : ZadarmaResponseModel
    {
        public string From { get; set; }
        public string To { get; set; }
        public DateTime Time { get; set; }
    }

    #endregion

    #region Response items

    public class PriceInfo : ICurrency
    {
        public string Prefix { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public string Currency { get; set; }
    }

    public class OverallStatisticsInfo : StatisticsInfo, ICurrency
    {
        public string Id { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Description { get; set; }
        public int BillSeconds { get; set; }
        public float Cost { get; set; }
        public float BillCost { get; set; }
        public string Currency { get; set; }
    }

    public class PBXStatisticsInfo : StatisticsInfo
    {
        public string Call_Id { get; set; }
        public string Clid { get; set; }
        public string Destination { get; set; }
        public int Seconds { get; set; }
        public bool Is_Recorded { get; set; }
        public string pbx_call_id { get; set; }
    }

    #endregion

    /// <summary>
    /// Service to provide access to Zadarma API
    /// </summary>
    public class ZadarmaService
    {
        private const string BaseAddress = "https://api.zadarma.com";
        private const string DateTimeTemplate = "yyyy-MM-dd HH:mm:ss";

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
            return $"/{ApiVersion}/{apiMethod}";
        }

        private string GetQueryString(IDictionary<string, string> parameters)
        {
            IOrderedEnumerable<KeyValuePair<string, string>> sorted = parameters.OrderBy(x => x.Key);
            return string.Join("&", sorted.Select(x => $"{x.Key}={WebUtility.UrlEncode(x.Value)}"));
        }

        /// <summary>
        /// Creates a ZadarmaResponseModel of type T
        /// </summary>
        /// <param name="content">Response content</param>
        /// <typeparam name="T">Type of the model to fill the successfully completed request</typeparam>
        /// <returns>Instance of T</returns>
        private async Task<ZadarmaResponseModel> ResolveRequestContentAsync<T>(HttpContent content)
            where T : ZadarmaResponseModel
        {
            var data = await content.ReadAsStringAsync();
            using (var sr = new StringReader(data))
            {
                var jtr = new JsonTextReader(sr);
                var serializer = new JsonSerializer();
                ZadarmaResponseModel model = serializer.Deserialize<T>(jtr);
                jtr.Close();
                return model;
            }
        }

        /// <summary>
        /// Creates a ZadarmaResponseModel of type T
        /// </summary>
        /// <param name="content">Response content</param>
        /// <param name="isSuccessStatusCode">The flag to define if the request has completed succesfully</param>
        /// <typeparam name="T">Type of the model to fill the successfully completed request</typeparam>
        /// <returns>Instance of T</returns>
        private async Task<ZadarmaResponseModel> ResolveRequestContentAsync<T>(HttpContent content,
            bool isSuccessStatusCode) where T : ZadarmaResponseModel
        {
            if (isSuccessStatusCode)
            {
                return await ResolveRequestContentAsync<T>(content);
            }
            return await ResolveRequestContentAsync<ErrorResponseModel>(content);
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
                
                Console.WriteLine(sign);
                if (!string.IsNullOrEmpty(queryString))
                {
                    requestUri = $"{requestUri}?{queryString}";
                }
                Console.WriteLine(requestUri);
                var request = new HttpRequestMessage(method, requestUri);
                request.Headers.TryAddWithoutValidation("Authorization", UserKey + ":" + sign);

                return await client.SendAsync(request);
            }
        }

        private DateTime GetBoundDateTime(DateTime? dateTime, BoundDateTimeKind kind)
        {
            DateTime dt = dateTime ?? DateTime.Today;
            if (kind == BoundDateTimeKind.Start)
            {
                return new DateTime(dt.Year, dt.Month, dateTime?.Day ?? 1, 0, 0, 0, DateTimeKind.Unspecified);
            }
            return new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59, DateTimeKind.Unspecified);
        }

        /// <summary>
        /// Get current balance of zadarma profile
        /// </summary>
        /// <returns>Something</returns>
        public async Task<ZadarmaResponseModel> GetBalanceAsync()
        {
            var response = await ExecuteRequestAsync(HttpMethod.Get, "info/balance");
            return await ResolveRequestContentAsync<BalanceResponseModel>(response.Content, response.IsSuccessStatusCode);
        }
        
        /// <summary>
        /// Get call price info to call a certain phone number
        /// </summary>
        /// <param name="number">A phone number to call</param>
        /// <returns></returns>
        public async Task<ZadarmaResponseModel> GetPriceInfoAsync(string number)
        {
            var response = await ExecuteRequestAsync(HttpMethod.Get, "info/price", new Dictionary<string, string> { { nameof(number), number } });
            return await ResolveRequestContentAsync<PriceInfoResponseModel>(response.Content, response.IsSuccessStatusCode);
        }

        /// <summary>
        /// Get overall statistics
        /// </summary>
        /// <param name="start">Start date of the statistics</param>
        /// <param name="end">End date of the statistics</param>
        /// <returns></returns>
        public async Task<ZadarmaResponseModel> GetOverallStatisticsAsync(DateTime? start = null, DateTime? end = null)
        {
            var parameters = new Dictionary<string, string>();

            start = GetBoundDateTime(start?.ToLocalTime(), BoundDateTimeKind.Start);
            parameters.Add(nameof(start), start.Value.ToString(DateTimeTemplate));

            end = GetBoundDateTime(end?.ToLocalTime(), BoundDateTimeKind.End);
            parameters.Add(nameof(end), end.Value.ToString(DateTimeTemplate));

            var response = await ExecuteRequestAsync(HttpMethod.Get, "statistics", parameters);
            return await ResolveRequestContentAsync<OverallStatisticsResponseModel>(response.Content, response.IsSuccessStatusCode);
        }

        /// <summary>
        /// Get PBX statitics
        /// </summary>
        /// <param name="start">Start date of the statistics</param>
        /// <param name="end">End date of the statistics</param>
        /// <param name="version">Format of the result: 2 - the new format, 1 - the old format</param>
        /// <returns></returns>
        public async Task<ZadarmaResponseModel> GetPBXStatisticsAsync(DateTime? start = null, DateTime? end = null, ResponseVersion version = ResponseVersion.New)
        {
            var parameters = new Dictionary<string, string> { { nameof(version), ((int)version).ToString() } };

            start = GetBoundDateTime(start?.ToLocalTime(), BoundDateTimeKind.Start);
            parameters.Add(nameof(start), start.Value.ToString(DateTimeTemplate));

            end = GetBoundDateTime(end?.ToLocalTime(), BoundDateTimeKind.End);
            parameters.Add(nameof(end), end.Value.ToString(DateTimeTemplate));

            var response = await ExecuteRequestAsync(HttpMethod.Get, "statistics/pbx", parameters);
            return await ResolveRequestContentAsync<PBXStatisticsResponseModel>(response.Content, response.IsSuccessStatusCode);
        }

        /// <summary>
        /// Send a request to callback
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="isPredicted">if the param is true, system will call "to" number first and then connect "from" on success</param>
        /// <returns></returns>
        public async Task<ZadarmaResponseModel> RequestCallbackAsync(string from, string to, bool isPredicted = true)
        {
            var parameters = new Dictionary<string, string> { {nameof(from), from}, {nameof(to), to} };
            var response = await ExecuteRequestAsync(HttpMethod.Get, "request/callback", parameters);
            return await ResolveRequestContentAsync<RequestCallbackResponseModel>(response.Content, response.IsSuccessStatusCode);
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