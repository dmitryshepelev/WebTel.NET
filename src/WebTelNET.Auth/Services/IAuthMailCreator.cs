using System.Collections.Generic;
using MimeKit;
using WebTelNET.Auth.Libs;
using WebTelNET.CommonNET.Services;

namespace WebTelNET.Auth.Services
{
    public interface IAuthMailCreator : IMailCreator
    {
        MimeMessage CreateAccountConfirmationMail(AccountConfirmationMailContext context, string sender, string reciever, string subject = null);
    }
}
