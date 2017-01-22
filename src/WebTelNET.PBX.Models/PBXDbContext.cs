using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using WebTelNET.PBX.Models.Models;

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

            modelBuilder.Entity<ZadarmaAccount>()
                .HasIndex(x => x.UserId).IsUnique();

            modelBuilder.Entity<PhoneNumber>()
                .HasIndex(x => new { x.Number, x.ZadarmaAccountId }).IsUnique();

            modelBuilder.Entity<Call>()
                .HasOne(x => x.Caller)
                .WithMany(x => x.Callers);
            modelBuilder.Entity<Call>()
                .HasOne(x => x.Destination)
                .WithMany(x => x.Destinations);
            modelBuilder.Entity<Call>()
                .Property(x => x.CallerId).IsRequired(false);
            modelBuilder.Entity<Call>()
                .Property(x => x.DestinationId).IsRequired(false);

            modelBuilder.Entity<Widget>()
                .HasIndex(x => x.UserId).IsUnique();
        }

        public DbSet<ZadarmaAccount> ZadarmaAccounts { get; set; }

        public DbSet<DispositionType> DispositionTypes { get; set; }
        public DbSet<NotificationType> NotificationTypes { get; set; }
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }
        public DbSet<Call> Calls { get; set; }
        public DbSet<Widget> Widgets { get;set; }
    }
}