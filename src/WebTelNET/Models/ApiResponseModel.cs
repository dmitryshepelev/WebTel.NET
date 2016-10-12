using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTelNET.Models
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
