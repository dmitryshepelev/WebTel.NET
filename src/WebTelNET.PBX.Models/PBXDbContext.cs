using Microsoft.EntityFrameworkCore;
using WebTelNET.PBX.Models.Models;

namespace WebTelNET.PBX.Models
{
    public class PBXDbContext : DbContext
    {
        public PBXDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ZadarmaAccount> ZadarmaAccounts { get; set; }
    }
}