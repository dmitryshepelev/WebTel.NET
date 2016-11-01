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

        public MimeMessage CreateAccountRequestConfirmationMail(
            AccountRequestConfirmationMailContext context,
            IList<string> senders,
            IList<string> recievers,
            string subject = null
        )
        {
            TemplateName = "AccountRequestConfirmationTemplate.cshtml";
            if (string.IsNullOrEmpty(subject))
            {
                subject = "Подтверждение запроса на создание аккаунта";
            }
            var message = Create(context, subject, senders, recievers);
            return message;
        }
    }
}
