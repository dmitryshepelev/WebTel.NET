using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTelNET.PBX.Models.Models
{
    public class ZadarmaAccount
    {
        public string Id { get; set; }

        public string UserKey { get; set; }
        public string SecretKey { get; set; }

        public string UserId { get; set; }
    }
}
