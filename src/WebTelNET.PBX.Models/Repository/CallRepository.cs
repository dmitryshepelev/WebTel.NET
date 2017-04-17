using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebTelNET.CommonNET.Libs.Repository;
using WebTelNET.PBX.Models.Models;

namespace WebTelNET.PBX.Models.Repository
{
    public interface ICallRepository : IRepository<Call>
    {
        IQueryable<Call> GetByAccountId(Guid id);
    }

    public class CallRepository : RepositoryBase<Call>, ICallRepository
    {
        public CallRepository(PBXDbContext context) : base(context)
        {
        }

        public IQueryable<Call> GetByAccountId(Guid id)
        {
            return GetAll(x => x.Caller != null && x.Caller.ZadarmaAccountId == id);
        }
    }
}