namespace WebTelNET.Office.Models.Libs
{
    public interface IServiceSettings
    {
        ServiceTypeNames ServiceTypeNames { get; set; }
        ServiceStatusNames ServiceStatusNames { get; set; }
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
}