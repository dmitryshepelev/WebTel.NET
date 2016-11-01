using System;
using WebTelNET.CommonNET.Libs;

namespace WebTelNET.Auth.Libs
{
    public class AccountRequestConfirmationMailContext : MailContext
    {
        public string Login { get; set; }
        public string RequestCode { get; set; }
        public DateTime DateTime { get; set; }
    }
}
