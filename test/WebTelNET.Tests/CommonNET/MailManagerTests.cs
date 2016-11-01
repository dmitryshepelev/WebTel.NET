using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MimeKit;
using WebTelNET.CommonNET.Libs;
using WebTelNET.CommonNET.Services;
using Xunit;

namespace WebTelNET.Tests.CommonNET
{
    public class MailManagerTests
    {
        [Fact]
        public void SendSuccess()
        {
            var mailManager = new MailManager();

            var message = new MimeMessage
            {
                Body = new TextPart("plain") {Text = "Test message! Greeting from the WebTelNET "},
                Subject = "Hello again"
            };
            message.From.Add(new MailboxAddress("Info Web-TelNET", "info@web-tel.ru"));
            message.To.Add(new MailboxAddress("Dmitry Shepelev", "dmitry.shepelev.ydx@yandex.ru"));

            mailManager.Send(message, new MailSettings());
        }
    }
}
