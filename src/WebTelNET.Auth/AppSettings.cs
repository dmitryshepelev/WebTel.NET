using WebTelNET.CommonNET.Libs;
using WebTelNET.Auth.Models.Libs;

namespace WebTelNET.Auth
{
    public class AppSettings : IMailSettings, IDatabaseSettings
    {
        public string LoginRedirect { get; set; }
        public bool IdentityProductionMode { get; set; }
        public DatabaseSettings DatabaseSettings { get; set; }
        public MailSettings MailSettings { get; set; }
    }
}
