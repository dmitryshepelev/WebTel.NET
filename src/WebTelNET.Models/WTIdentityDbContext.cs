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
            base.OnModelCreating(builder);
            builder.Entity<AccountRequest>()
                .Property(x => x.IsComfirmed)
                .HasDefaultValue(false);
            builder.Entity<AccountRequest>()
                .HasIndex(x => new { x.RequestCode, x.Email, x.Login })
                .IsUnique();
        }

        public DbSet<AccountRequest> AccountRequests { get; set; }
    }
}
