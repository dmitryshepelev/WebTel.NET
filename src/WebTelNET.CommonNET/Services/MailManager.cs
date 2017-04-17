using MailKit.Net.Smtp;
using MimeKit;
using WebTelNET.CommonNET.Libs;

namespace WebTelNET.CommonNET.Services
{
    public class MailManager : IMailManager
    {
        public void Send(MimeMessage message, MailSettings settings)
        {
            using (var client = new SmtpClient())
            {
                client.LocalDomain = settings.LocalDomain;
                client.Connect(settings.SMTPServer, settings.Port);
                client.Authenticate(settings.Login, settings.Password);
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}