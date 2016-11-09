using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using MimeKit;
using WebTelNET.Auth.Libs;
using WebTelNET.CommonNET.Services;

namespace WebTelNET.Auth.Services
{
    public class AuthMailCreator : MailCreator, IAuthMailCreator
    {
        public AuthMailCreator(IHostingEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
        }

        public MimeMessage CreateAccountConfirmationMail(
            AccountConfirmationMailContext context,
            string sender,
            string reciever,
            string subject = null
        )
        {
            TemplateName = "AccountConfirmationTemplate.cshtml";
            if (string.IsNullOrEmpty(subject))
            {
                subject = "Подтверждение cоздания аккаунта";
            }
            var message = Create(context, subject, new List<string> { sender }, new List<string> { reciever });
            return message;
        }
    }
}
