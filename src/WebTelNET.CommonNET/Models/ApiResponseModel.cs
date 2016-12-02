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

    public class ApiNotAuthorizedResponseModel : ApiResponseModel
    {
        public ApiNotAuthorizedResponseModel() : base()
        {
            this.Message = "The request isn't authorized to use the api.";
        }
    }
}
