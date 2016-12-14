using WebTelNET.CommonNET.Libs.Repository;
using WebTelNET.PBX.Models.Models;

namespace WebTelNET.PBX.Models.Repository
{
    public interface IZadarmaAccountRepository
    {
        ZadarmaAccount GetAccount(string userId);
    }

    public class ZadarmaAccountRepository : RepositoryBase<ZadarmaAccount>, IZadarmaAccountRepository
    {
        public ZadarmaAccountRepository(PBXDbContext context) : base(context)
        {
        }

        public ZadarmaAccount GetAccount(string userId)
        {
            return GetSingle(x => x.UserId.Equals(userId));
        }
    }
}
