using System.Collections.Generic;
using WebTelNET.Office.Libs.Models;

namespace WebTelNET.Office.Models
{
    public class ActivationUserServiceRequestModel
    {
        public string ServiceTypeName { get; set; }
        public Dictionary<string, string> ActivationData { get; set; }
    }

    public class UserServiceResponseModel : ServiceInfoResponseModel
    {
        public bool RequireActivationData { get; set; }
        public ServiceProviderResponseModel Provider { get; set; }
    }
}