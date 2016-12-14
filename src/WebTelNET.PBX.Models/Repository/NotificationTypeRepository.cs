using Microsoft.EntityFrameworkCore;
using WebTelNET.CommonNET.Libs.Repository;
using WebTelNET.PBX.Models.Models;

namespace WebTelNET.PBX.Models.Repository
{
    public interface INotificationTypeRepository : IRepository<NotificationType>
    {

    }

    public class NotificationTypeRepository : RepositoryBase<NotificationType>, INotificationTypeRepository
    {
        public NotificationTypeRepository(PBXDbContext context) : base(context)
        {
        }
    }
}