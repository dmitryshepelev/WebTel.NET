using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebTelNET.Models.Models;

namespace WebTelNET.Models
{
    public class WTIdentityDbContext : IdentityDbContext<WTUser, WTRole, string>
    {
        public WTIdentityDbContext(DbContextOptions<WTIdentityDbContext> options) : base(options)
        {
        }
    }
}
