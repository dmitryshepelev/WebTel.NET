using Microsoft.EntityFrameworkCore;
using WebTelNET.CommonNET.Libs.Repository;
using WebTelNET.PBX.Models.Models;

namespace WebTelNET.PBX.Models.Repository
{
    public interface IWidgetRepository : IRepository<Widget>
    {
        Widget GetOrCreate(string userId);
    }

    public class WidgetRepository : RepositoryBase<Widget>, IWidgetRepository
    {
        public WidgetRepository(PBXDbContext context) : base(context)
        {
        }


        public Widget GetOrCreate(string userId)
        {
            var widget = GetSingle(x => x.UserId.Equals(userId));
            if (widget == null)
            {
                var instance = new Widget {UserId = userId};
                return Create(instance);
            }
            return widget;
        }
    }
}