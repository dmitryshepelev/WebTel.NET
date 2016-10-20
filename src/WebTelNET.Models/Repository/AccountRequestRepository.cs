using System;
using WebTelNET.Models.Models;

namespace WebTelNET.Models.Repository
{
    public class AccountRequestRepository : RepositoryBase<AccountRequest>, IAccountRequestRepository
    {
        public AccountRequestRepository(WTIdentityDbContext context) : base(context)
        {
        }

        public override AccountRequest Create(AccountRequest entity)
        {
            entity.RequestCode = Guid.NewGuid().ToString();
            return base.Create(entity);
        }
    }
}