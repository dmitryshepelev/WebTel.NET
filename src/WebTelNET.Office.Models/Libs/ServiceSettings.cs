namespace WebTelNET.Office.Models.Libs
{
    public interface IServiceSettings
    {
        ServiceTypeNames ServiceTypeNames { get; set; }
        ServiceStatusNames ServiceStatusNames { get; set; }
        ServiceProviderTypeSettings ServiceProviderTypeSettings { get; set; }
    }

    public class ServiceTypeNames
    {
        public string PBXType { get; set; }
    }

    public class ServiceStatusNames
    {
        public string Available { get; set; }
        public string Activated { get; set; }
        public string Unavailable { get; set; }
    }

    public class ServiceProviderTypeSettings
    {
        public ServiceProviderSettings PBX { get; set; }
    }

    public class ServiceProviderSettings
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string WebSite { get; set; }
    }
}