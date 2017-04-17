using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTelNET.CommonNET.Libs;

namespace WebTelNET.PBX
{
    public class AppSettings : IMailSettings
    {
        public MailSettings MailSettings { get; set; }
    }
}
