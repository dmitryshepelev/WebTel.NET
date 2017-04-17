using System.Collections.Generic;
using WebTelNET.Office.Models.Libs;

namespace WebTelNET.Office
{
    public class AppSettings : IServiceSettings
    {
        public ServiceTypeNames ServiceTypeNames { get; set; }
        public ServiceStatusesSettings ServiceStatusesSettings { get; set; }
        public ServiceProviderTypeSettings ServiceProviderTypeSettings { get; set; }
    }
}