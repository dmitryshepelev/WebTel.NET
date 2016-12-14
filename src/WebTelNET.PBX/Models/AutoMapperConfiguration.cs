using AutoMapper;
using WebTelNET.PBX.Models.Models;

namespace WebTelNET.PBX.Models
{
    public class AutoMapperConfiguration : Profile
    {
        protected override void Configure()
        {
            base.Configure();
            CreateMap<IncomingCallStartNotificationModel, IncomingCallNotification>();
        }
    }
}