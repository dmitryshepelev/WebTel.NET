using System;
using System.Collections.Generic;
using WebTelNET.CommonNET.Libs;

namespace WebTelNET.PBX.Services
{
    public interface IOfficeService
    {
    }

    public class OfficeService : HttpService, IOfficeService
    {
        public OfficeService() : base("http://localhost:5002/api/office/")
        {
        }
    }
}