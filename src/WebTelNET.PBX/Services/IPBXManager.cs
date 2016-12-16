using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using WebTelNET.PBX.Models.Models;

namespace WebTelNET.PBX.Services
{
    public interface IPBXManager
    {
        Call ProcessCallNotification(JObject model, Guid zadarmaAccountId);
    }
}