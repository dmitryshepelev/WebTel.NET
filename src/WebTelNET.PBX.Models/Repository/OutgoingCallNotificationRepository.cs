using Microsoft.EntityFrameworkCore;
using WebTelNET.CommonNET.Libs.Repository;
using WebTelNET.PBX.Models.Models;

namespace WebTelNET.PBX.Models.Repository
{
    public interface IOutgoingCallNotificationRepository : IRepository<OutgoingCallNotification>
    {

    }

    public class OutgoingCallNotificationRepository : RepositoryBase<OutgoingCallNotification>, IOutgoingCallNotificationRepository
    {
        public OutgoingCallNotificationRepository(PBXDbContext context) : base(context)
        {
        }
    }
}