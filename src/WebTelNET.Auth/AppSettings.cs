using WebTelNET.CommonNET.Libs;
using WebTelNET.Models.Libs;

namespace WebTelNET.Auth
{
    public class AppSettings : DatabaseSettings
    {
        public MailSettings MailSettings { get; set; }
        public string LoginRedirect { get; set; }
    }
}
