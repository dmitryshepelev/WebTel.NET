using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using MimeKit;
using RazorLight;
using WebTelNET.CommonNET.Libs;

namespace WebTelNET.CommonNET.Services
{
    public class MailCreator : IMailCreator
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public MailCreator(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            TemplateName = "DefaultMail.cshtml";
            TemplatePath = "/Views/Templates/";
        }

        public string TemplateName { get; protected set; }
        public string TemplatePath { get; protected set; }

        public virtual MimeMessage Create(MailContext context)
        {
            if (context == null)
            {
                context = new MailContext();
            }
            var body = ParseTemplate(TemplatePath, TemplateName, context);
            var message = new MimeMessage
            {
                Body = new TextPart("html") {Text = body}
            };
            return message;
        }

        public virtual MimeMessage Create(MailContext context, string subject)
        {
            var message = Create(context);
            message.Subject = subject;
            return message;
        }

        public virtual MimeMessage Create(MailContext context, string subject, IList<string> senders, IList<string> recievers)
        {
            var message = Create(context, subject);
            foreach (var reciever in recievers)
            {
                message.To.Add(new MailboxAddress(reciever));
            }
            foreach (var sender in senders)
            {
                message.From.Add(new MailboxAddress(sender));
            }
            return message;
        }

        protected virtual string ParseTemplate(string templatePath, string templateName, MailContext context)
        {
            var engine = EngineFactory.CreatePhysical($"{_hostingEnvironment.ContentRootPath}{templatePath}");
            return engine.Parse(templateName, context);
        }
    }
}
