using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebTelNET.Auth.Models.Libs;


namespace WebTelNET.Auth.Models
{
    public class AuthDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public async void EnsureSeedData(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, DatabaseSettings databaseSettings)
        {
            if (await roleManager.FindByNameAsync(databaseSettings.RoleSettings.UserRole) == null)
            {
                await roleManager.CreateAsync(new IdentityRole {Name = databaseSettings.RoleSettings.UserRole});
            }
            if (await roleManager.FindByNameAsync(databaseSettings.RoleSettings.AdminRole) == null)
            {
                await roleManager.CreateAsync(new IdentityRole { Name = databaseSettings.RoleSettings.AdminRole });
            }
        }
    }
}
