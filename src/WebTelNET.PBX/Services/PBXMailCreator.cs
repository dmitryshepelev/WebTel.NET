using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using MimeKit;
using WebTelNET.CommonNET.Libs;
using WebTelNET.CommonNET.Services;

namespace WebTelNET.PBX.Services
{
    public interface IPBXMailCreator : IMailCreator
    {

    }

    public class PBXMailCreator : MailCreator, IPBXMailCreator
    {
        public PBXMailCreator(IHostingEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
        }
    }
}
