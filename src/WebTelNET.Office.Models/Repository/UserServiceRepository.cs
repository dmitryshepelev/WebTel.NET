using Microsoft.EntityFrameworkCore;
using WebTelNET.CommonNET.Libs.Repository;
using WebTelNET.Office.Models.Models;

namespace WebTelNET.Office.Models.Repository
{
    public interface IUserServcieRepository : IRepository<UserService>
    {
        
    }
    
    public class UserServiceRepository : RepositoryBase<UserService>, IUserServcieRepository
    {
        public UserServiceRepository(OfficeDbContext context) : base(context)
        {
        }
    }
}