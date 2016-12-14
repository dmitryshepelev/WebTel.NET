using Microsoft.EntityFrameworkCore;
using WebTelNET.CommonNET.Libs.Repository;
using WebTelNET.PBX.Models.Models;

namespace WebTelNET.PBX.Models.Repository
{
    public interface IDispositionTypeRepository : IRepository<DispositionType>
    {

    }

    public class DispositionTypeRepository : RepositoryBase<DispositionType>, IDispositionTypeRepository
    {
        public DispositionTypeRepository(PBXDbContext context) : base(context)
        {
        }
    }
}