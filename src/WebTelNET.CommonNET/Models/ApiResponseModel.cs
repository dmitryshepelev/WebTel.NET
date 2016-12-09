using System.Collections.Generic;

namespace WebTelNET.CommonNET.Models
{
    public class ApiResponseModel
    {
        public string Message { get; set; }
        public IDictionary<string, object> Data { get; set; }

        public ApiResponseModel()
        {
            this.Data = new Dictionary<string, object>();
        }
    }
}
