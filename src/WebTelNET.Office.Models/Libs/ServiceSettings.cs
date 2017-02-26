using System.Collections.Generic;

namespace WebTelNET.Office.Models.Libs
{
    public interface IServiceSettings
    {
        ServiceTypeNames ServiceTypeNames { get; set; }
        ServiceStatusesSettings ServiceStatusesSettings { get; set; }
        ServiceProviderTypeSettings ServiceProviderTypeSettings { get; set; }
    }

    public class ServiceTypeNames
    {
        public string PBXType { get; set; }
        public string CloudStorageType { get; set; }
    }

    public class ServiceStatusesSettings
    {
        public ServiceStatusSettings Available { get; set; }
        public ServiceStatusSettings Activated { get; set; }
        public ServiceStatusSettings Unavailable { get; set; }
    }

    public class ServiceStatusSettings
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class ServiceProviderTypeSettings
    {
        public ServiceProviderSettings PBX { get; set; }
        public ServiceProviderSettings CloudStorage { get; set; }
    }

    public class ServiceProviderSettings
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string WebSite { get; set; }
        public Dictionary<string, string> UserData { get; set; }
    }
}