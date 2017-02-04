using Microsoft.EntityFrameworkCore;
using WebTelNET.CommonNET.Libs.Repository;
using WebTelNET.Office.Models.Models;

namespace WebTelNET.Office.Models.Repository
{
    public interface IServiceProviderRepository : IRepository<ServiceProvider>
    {
        ServiceProvider GetByType(string serviceTypeName);
    }

    public class ServiceProviderRepository : RepositoryBase<ServiceProvider>, IServiceProviderRepository
    {
        public ServiceProviderRepository(OfficeDbContext context) : base(context)
        {
        }

        public ServiceProvider GetByType(string serviceTypeName)
        {
            return GetSingle(x => x.ServiceType.Name.Equals(serviceTypeName));
        }
    }
}