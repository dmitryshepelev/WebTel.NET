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

        protected override void OnModelCreating(ModelBuilder builder)
        {
        }
    }
}
