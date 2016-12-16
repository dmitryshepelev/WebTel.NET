using AutoMapper;
using WebTelNET.PBX.Models;
using WebTelNET.PBX.Models.Models;
using WebTelNET.PBX.Services;

namespace WebTelNET.PBX.Libs
{
    public class AutoMapperConfiguration : Profile
    {
        protected override void Configure()
        {
            CreateMap<IncomingCallStartRequestModel, Call>()
                .ForMember(m => m.NotificationTypeId, o => o.MapFrom(s => (int) ZadarmaService.ParseNotificationType(s.Event)))
                .ForMember(m => m.CallStart, o => o.MapFrom(s => s.call_start))
                .ForMember(m => m.PBXCallId, o => o.MapFrom(s => s.pbx_call_id))
                .ForMember(m => m.Destination, o => o.MapFrom(s => s.called_did));

            CreateMap<Call, IncomingCallViewModel>()
                .ForMember(m => m.NotificationType, o => o.MapFrom(s => s.NotificationTypeId))
                .ForMember(m => m.CallerId, o => o.MapFrom(s => s.Caller != null ? s.Caller.Number : null ));
        }
    }
}