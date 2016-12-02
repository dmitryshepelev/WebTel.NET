using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WebTelNET.Models.Models
{
    public class WTUser : IdentityUser
    {
        public string ZadarmaAccountId { get; set; }
        public ZadarmaAccount ZadarmaAccount { get; set; }
    }
}
