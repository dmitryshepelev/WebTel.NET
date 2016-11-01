using System.Threading.Tasks;
using MimeKit;
using WebTelNET.CommonNET.Libs;

namespace WebTelNET.CommonNET.Services
{
    public interface IMailManager
    {
        void Send(MimeMessage message, MailSettings settings);
    }
}