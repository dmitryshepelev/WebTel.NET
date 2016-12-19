using WebTelNET.CommonNET.Libs.Repository;
using WebTelNET.PBX.Models.Models;

namespace WebTelNET.PBX.Models.Repository
{
    public interface ICallerRepository : IRepository<Caller>
    {
        Caller GetOrCreate(Caller caller);
    }

    public class CallerRepository : RepositoryBase<Caller>, ICallerRepository
    {
        public CallerRepository(PBXDbContext context) : base(context)
        {
        }

        public Caller GetOrCreate(Caller caller)
        {
            var existingCaller =
                GetSingle(x => x.Number.Equals(caller.Number) && x.ZadarmaAccountId == caller.ZadarmaAccountId);

            return existingCaller ?? Create(caller);
        }
    }
}