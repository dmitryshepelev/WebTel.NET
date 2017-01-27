using Microsoft.EntityFrameworkCore;
using WebTelNET.CommonNET.Libs.Repository;
using WebTelNET.Office.Models.Models;

namespace WebTelNET.Office.Models.Repository
{
    public interface IServiceProviderRepository : IRepository<ServiceProvider>
    {

    }

    public class ServiceProviderRepository : RepositoryBase<ServiceProvider>, IServiceProviderRepository
    {
        public ServiceProviderRepository(OfficeDbContext context) : base(context)
        {
        }
    }
}