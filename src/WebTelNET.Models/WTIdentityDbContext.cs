using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebTelNET.Models.Libs;
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

            builder.Entity<WTUser>()
                .HasOne(q => q.ZadarmaAccount)
                .WithOne(v => v.User)
                .HasForeignKey<WTUser>(q => q.ZadarmaAccountId);
        }

        public async void EnsureSeedData(UserManager<WTUser> userManager, RoleManager<WTRole> roleManager, DatabaseSettings databaseSettings)
        {
            if (await roleManager.FindByNameAsync(databaseSettings.RoleSettings.UserRole) == null)
            {
                await roleManager.CreateAsync(new WTRole {Name = databaseSettings.RoleSettings.UserRole});
            }
            if (await roleManager.FindByNameAsync(databaseSettings.RoleSettings.AdminRole) == null)
            {
                await roleManager.CreateAsync(new WTRole { Name = databaseSettings.RoleSettings.AdminRole });
            }
        }
    }
}
