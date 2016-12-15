using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using WebTelNET.PBX.Models.Models;
using WebTelNET.PBX.Models.Repository;

namespace WebTelNET.PBX.Models
{
    public class PBXDbContext : DbContext
    {
        public PBXDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasPostgresExtension("uuid-ossp");

            modelBuilder.Entity<Caller>()
                .HasIndex(x => x.Number)
                .IsUnique();
        }

        public DbSet<ZadarmaAccount> ZadarmaAccounts { get; set; }

        public DbSet<DispositionType> DispositionTypes { get; set; }
        public DbSet<NotificationType> NotificationTypes { get; set; }
        public DbSet<Caller> Callers { get; set; }
        public DbSet<OutgoingCallNotification> OutgoingCallNotifications { get; set; }
        public DbSet<IncomingCallNotification> IncomingCallNotifications { get; set; }
    }
}