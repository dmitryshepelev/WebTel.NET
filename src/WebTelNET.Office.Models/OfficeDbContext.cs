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

            modelBuilder.Entity<ServiceType>()
                .HasOne(x => x.ServiceProvider)
                .WithOne(x => x.ServiceType)
                .HasForeignKey<ServiceProvider>(x => x.ServiceTypeId);

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

        public void EnsureSeedData(
            IServiceStatusRepository serviceStatusRepository,
            IServiceTypeRepository serviceTypeRepository,
            IServiceProviderRepository serviceProviderRepository,
            IServiceSettings serviceSettings
        )
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

            properties = serviceSettings.ServiceStatusesSettings.GetType().GetProperties();
            foreach (var property in properties)
            {
                var value = serviceSettings.ServiceStatusesSettings.GetType()
                    .GetProperty(property.Name)
                    .GetValue(serviceSettings.ServiceStatusesSettings);

                var typedValue = (ServiceStatusSettings) value;

                var serviceStatus = serviceStatusRepository.GetSingle(x => x.Name.Equals(typedValue.Name));
                if (serviceStatus == null)
                {
                    serviceStatusRepository.Create(new ServiceStatus
                    {
                        Name = typedValue.Name,
                        Description = typedValue.Description
                    });
                }
                else
                {
                    serviceStatus.Name = typedValue.Name;
                    serviceStatus.Description = typedValue.Description;
                    serviceStatusRepository.Update(serviceStatus);
                }
            }

            properties = serviceSettings.ServiceProviderTypeSettings.GetType().GetProperties();
            foreach (var property in properties)
            {
                var value = serviceSettings.ServiceProviderTypeSettings.GetType()
                    .GetProperty(property.Name)
                    .GetValue(serviceSettings.ServiceProviderTypeSettings);

                var typedValue = (ServiceProviderSettings) value;

                var serviceProvider = serviceProviderRepository.GetSingle(x => x.Name.Equals(typedValue.Name));
                if (serviceProvider == null)
                {
                    var serviceType = serviceTypeRepository.GetSingle(x => x.Name.Equals(property.Name));
                    if (serviceType == null)
                    {
                        continue;
                    }

                    serviceProviderRepository.Create(new ServiceProvider
                    {
                        Name = typedValue.Name,
                        Description = typedValue.Description,
                        WebSite = typedValue.WebSite,
                        ServiceTypeId = serviceType.Id
                    });
                }
                else
                {
                    serviceProvider.Name = typedValue.Name;
                    serviceProvider.Description = typedValue.Description;
                    serviceProvider.WebSite = typedValue.WebSite;
                    serviceProviderRepository.Update(serviceProvider);
                }
            }
        }
    }
}