using Microsoft.AspNetCore.Hosting;
using WebTelNET.CommonNET.Services;

namespace WebTelNET.Office.Services
{
    public interface IOfficeMailCreator : IMailCreator
    {

    }

    public class OfficeMailCreator : MailCreator, IOfficeMailCreator
    {
        public OfficeMailCreator(IHostingEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
        }
    }
}