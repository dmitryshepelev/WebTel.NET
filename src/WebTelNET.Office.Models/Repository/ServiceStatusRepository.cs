using Microsoft.EntityFrameworkCore;
using WebTelNET.CommonNET.Libs.Repository;
using WebTelNET.Office.Models.Models;

namespace WebTelNET.Office.Models.Repository
{
    public interface IServiceStatusRepository : IRepository<ServiceStatus>
    {

    }

    public class ServiceStatusRepository : RepositoryBase<ServiceStatus>, IServiceStatusRepository
    {
        public ServiceStatusRepository(OfficeDbContext context) : base(context)
        {
        }
    }
}