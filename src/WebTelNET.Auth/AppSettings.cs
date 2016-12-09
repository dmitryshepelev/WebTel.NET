using WebTelNET.CommonNET.Libs;
using WebTelNET.Models.Libs;

namespace WebTelNET.Auth
{
    public class AppSettings : IMailSettings, IDatabaseSettings
    {
        public string LoginRedirect { get; set; }
        public DatabaseSettings DatabaseSettings { get; set; }
        public MailSettings MailSettings { get; set; }
    }
}
