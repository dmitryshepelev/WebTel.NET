using Microsoft.EntityFrameworkCore;
using WebTelNET.CommonNET.Libs.Repository;
using WebTelNET.Office.Models.Models;

namespace WebTelNET.Office.Models.Repository
{
    public interface IServiceTypeRepository : IRepository<ServiceType>
    {

    }

    public class ServiceTypeRepository : RepositoryBase<ServiceType>, IServiceTypeRepository
    {
        public ServiceTypeRepository(OfficeDbContext context) : base(context)
        {
        }
    }
}