using Microsoft.EntityFrameworkCore;
using WebTelNET.CommonNET.Libs.Repository;
using WebTelNET.PBX.Models.Models;

namespace WebTelNET.PBX.Models.Repository
{
    public interface IIncomingCallNotificationRepository : IRepository<IncomingCallNotification>
    {

    }

    public class IncomingCallNotificationRepository : RepositoryBase<IncomingCallNotification>, IIncomingCallNotificationRepository
    {
        public IncomingCallNotificationRepository(PBXDbContext context) : base(context)
        {
        }
    }
}