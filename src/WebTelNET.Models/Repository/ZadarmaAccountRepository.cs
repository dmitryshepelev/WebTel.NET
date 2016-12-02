using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTelNET.Models.Models;

namespace WebTelNET.Models.Repository
{
    public class ZadarmaAccountRepository : RepositoryBase<ZadarmaAccount>, IZadarmaAccountRepository
    {
        public ZadarmaAccountRepository(WTIdentityDbContext context) : base(context)
        {
        }

        public ZadarmaAccount GetAccount(string userId)
        {
            return GetSingle(x => x.UserId.Equals(userId));
        }
    }
}
