using Microsoft.EntityFrameworkCore;
using WebTelNET.PBX.Models.Models;
using WebTelNET.PBX.Models.Repository;

namespace WebTelNET.PBX.Models
{
    public class PBXDbContext : DbContext
    {
        public PBXDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ZadarmaAccount> ZadarmaAccounts { get; set; }

        public DbSet<DispositionType> DispositionTypes { get; set; }
        public DbSet<NotificationType> NotificationTypes { get; set; }
        public DbSet<OutgoingCallNotification> OutgoingCallNotifications { get; set; }
        public DbSet<IncomingCallNotification> IncomingCallNotifications { get; set; }
    }
}