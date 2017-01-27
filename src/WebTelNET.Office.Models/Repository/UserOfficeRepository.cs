using Microsoft.EntityFrameworkCore;
using WebTelNET.CommonNET.Libs.Repository;
using WebTelNET.Office.Models.Models;

namespace WebTelNET.Office.Models.Repository
{
    public interface IUserOfficeRepository : IRepository<UserOffice>
    {

    }

    public class UserOfficeRepository : RepositoryBase<UserOffice>, IUserOfficeRepository
    {
        public UserOfficeRepository(OfficeDbContext context) : base(context)
        {
        }
    }
}