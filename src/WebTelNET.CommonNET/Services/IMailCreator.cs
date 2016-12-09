using System.Collections.Generic;
using MimeKit;
using WebTelNET.CommonNET.Libs;

namespace WebTelNET.CommonNET.Services
{
    public interface IMailCreator : IErrorMailCreator
    {
        MimeMessage Create(MailContext context);
        MimeMessage Create(MailContext context, string subject);
        MimeMessage Create(MailContext context, string subject, IList<string> senders, IList<string> recievers);
    }
}
