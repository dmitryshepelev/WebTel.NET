using WebTelNET.Models.Models;

namespace WebTelNET.Models.Repository
{
    public class AccountRequestRepository : RepositoryBase<AccountRequest>, IAccountRequestRepository
    {
        public AccountRequestRepository(WTIdentityDbContext context) : base(context)
        {
        }
    }
}