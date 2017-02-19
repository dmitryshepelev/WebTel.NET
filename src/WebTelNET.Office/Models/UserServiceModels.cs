using WebTelNET.Office.Libs.Models;

namespace WebTelNET.Office.Models
{
    public class UserServiceResponseModel : ServiceInfoResponseModel
    {
        public ServiceProviderResponseModel Provider { get; set; }
    }
}