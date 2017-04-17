namespace WebTelNET.CommonNET.Libs
{
    public class AppSettings : IMailSettings
    {
        public MailSettings MailSettings { get; set; }
    }

    public interface IMailSettings
    {
        MailSettings MailSettings { get; set; }
    }

    public class MailSettings
    {
        public string LocalDomain { get; set; }
        public string SMTPServer { get; set; }
        public int Port { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
