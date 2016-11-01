using System.Collections.Generic;
using MimeKit;
using WebTelNET.Auth.Libs;
using WebTelNET.CommonNET.Services;

namespace WebTelNET.Auth.Services
{
    public interface IAuthMailCreator : IMailCreator
    {
        MimeMessage CreateAccountRequestConfirmationMail(AccountRequestConfirmationMailContext context, IList<string> senders, IList<string> recievers, string subject = null);
    }
}
