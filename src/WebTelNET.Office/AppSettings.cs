using System.Collections.Generic;
using WebTelNET.Office.Models.Libs;

namespace WebTelNET.Office
{
    public class AppSettings : IServiceSettings
    {
        public ServiceTypeNames ServiceTypeNames { get; set; }
        public ServiceStatusNames ServiceStatusNames { get; set; }
    }
}