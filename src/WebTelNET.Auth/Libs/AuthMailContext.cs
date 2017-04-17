using System;
using WebTelNET.Auth.Models;
using WebTelNET.CommonNET.Libs;

namespace WebTelNET.Auth.Libs
{
    public class AccountConfirmationMailContext : MailContext
    {
        public SignUpViewModel SignUpViewModel { get; set; }
        public DateTime DateTime { get; set; }
    }
}
