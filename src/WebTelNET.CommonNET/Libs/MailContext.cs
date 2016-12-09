using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTelNET.CommonNET.Libs
{
    public class MailContext
    {
    }

    public class ErrorMailContext : MailContext
    {
        public DateTime DateTime { get; set; }
        public string ErrorType { get; set; }
        public string StackTrace { get; set; }
    }
}
