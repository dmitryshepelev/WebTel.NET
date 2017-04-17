using System.Collections.Generic;
using MimeKit;
using WebTelNET.CommonNET.Libs;

namespace WebTelNET.CommonNET.Services
{
    public interface IErrorMailCreator
    {
        MimeMessage CreateErrorMessage(MailContext context, string subject, IList<string> senders,
            IList<string> recievers);
    }
}