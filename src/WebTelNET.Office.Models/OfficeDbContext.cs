using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using WebTelNET.Office.Models.Libs;
using WebTelNET.Office.Models.Models;
using WebTelNET.Office.Models.Repository;

namespace WebTelNET.Office.Models
{
    public class OfficeDbContext : DbContext
    {
        public OfficeDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasPostgresExtension("uuid-ossp");

            modelBuilder.Entity<UserOffice>()
                .HasIndex(x => x.UserId)
                .IsUnique();
            modelBuilder.Entity<UserOffice>()
                .HasMany(x => x.Services)
                .WithOne(x => x.UserOffice);

            modelBuilder.Entity<ServiceProvider>()
                .HasIndex(x => x.Name)
                .IsUnique();

            modelBuilder.Entity<ServiceStatus>()
                .HasIndex(x => x.Name)
                .IsUnique();

            modelBuilder.Entity<UserService>()
                .HasIndex(x => new {x.UserOfficeId, x.ServiceProviderId})
                .IsUnique();
            modelBuilder.Entity<UserService>()
                .Property(x => x.ServiceStatusId)
                .HasDefaultValue(1);
        }

        public DbSet<UserOffice> UserOffices { get; set; }
        public DbSet<UserService> UserServices { get; set; }
        public DbSet<ServiceType> ServiceTypes { get; set; }
        public DbSet<ServiceStatus> ServiceStatuses { get; set; }
        public DbSet<ServiceProvider> ServiceProviders { get; set; }

        public void EnsureSeedData(IServiceStatusRepository serviceStatusRepository, IServiceTypeRepository serviceTypeRepository, IServiceSettings serviceSettings)
        {
            PropertyInfo[] properties;

            properties = serviceSettings.ServiceTypeNames.GetType().GetProperties();
            foreach (var property in properties)
            {
                var value = serviceSettings.ServiceTypeNames.GetType()
                    .GetProperty(property.Name)
                    .GetValue(serviceSettings.ServiceTypeNames);

                string typedValue = (string) value;

                if (serviceTypeRepository.GetSingle(x => x.Name.Equals(typedValue)) == null)
                {
                    serviceTypeRepository.Create(new ServiceType { Name = typedValue });
                }
            }

            properties = serviceSettings.ServiceStatusNames.GetType().GetProperties();
            foreach (var property in properties)
            {
                var value = serviceSettings.ServiceStatusNames.GetType()
                    .GetProperty(property.Name)
                    .GetValue(serviceSettings.ServiceStatusNames);

                string typedValue = (string) value;

                if (serviceStatusRepository.GetSingle(x => x.Name.Equals(typedValue)) == null)
                {
                    serviceStatusRepository.Create(new ServiceStatus { Name = typedValue });
                }
            }
        }
    }
}