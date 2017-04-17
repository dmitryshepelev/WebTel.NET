using Microsoft.EntityFrameworkCore;
using WebTelNET.CommonNET.Libs.Repository;
using WebTelNET.Office.Models.Models;

namespace WebTelNET.Office.Models.Repository
{
    public interface IUserServiceDataRepository : IRepository<UserServiceData>
    {

    }

    public class UserServiceDataRepository : RepositoryBase<UserServiceData>, IUserServiceDataRepository
    {
        public UserServiceDataRepository(OfficeDbContext context) : base(context)
        {
        }
    }
}