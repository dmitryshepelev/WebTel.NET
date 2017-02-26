using WebTelNET.Office.Libs.Models;

namespace WebTelNET.Office.Models
{
    public class ActivationUserServiceRequestModel
    {
        public string ServiceTypeName { get; set; }
    }

    public class UserServiceResponseModel : ServiceInfoResponseModel
    {
        public bool RequireData { get; set; }
        public ServiceProviderResponseModel Provider { get; set; }
    }
}