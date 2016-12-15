using WebTelNET.CommonNET.Libs.Repository;
using WebTelNET.PBX.Models.Models;

namespace WebTelNET.PBX.Models.Repository
{
    public interface ICallerRepository : IRepository<Caller>
    {
        Caller GetOrCreate(Caller caller);
        Caller GetByNumber(string number);
    }

    public class CallerRepository : RepositoryBase<Caller>, ICallerRepository
    {
        public CallerRepository(PBXDbContext context) : base(context)
        {
        }

        public Caller GetOrCreate(Caller caller)
        {
            var existingCaller = GetSingle(x => x.Id == caller.Id) ?? GetByNumber(caller.Number);
            return existingCaller ?? Create(caller);
        }

        public Caller GetByNumber(string number)
        {
            return GetSingle(x => x.Number.Equals(number));
        }
    }
}