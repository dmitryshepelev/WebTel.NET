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

            CreateMap<IncomingCallEndRequestModel, Call>()
                .ForMember(m => m.NotificationTypeId,
                    o => o.MapFrom(s => (int) ZadarmaService.ParseNotificationType(s.Event)))
                .ForMember(m => m.CallStart, o => o.MapFrom(s => s.call_start))
                .ForMember(m => m.PBXCallId, o => o.MapFrom(s => s.pbx_call_id))
                .ForMember(m => m.Destination, o => o.MapFrom(s => s.called_did))
                .ForMember(m => m.Internal, o => o.MapFrom(s => s.Internal))
                .ForMember(m => m.Duration, o => o.MapFrom(s => s.duration))
                .ForMember(m => m.DispositionTypeId, o => o.MapFrom(s => (int) ZadarmaService.ParseDispositionType(s.disposition)))
                .ForMember(m => m.StatusCode, o => o.MapFrom(s => s.status_code))
                .ForMember(m => m.IsRecorded, o => o.MapFrom(s => s.is_recorded))
                .ForMember(m => m.CallIdWithRecord, o => o.MapFrom(s => s.call_id_with_rec));

            CreateMap<OutgoingCallStartRequestModel, Call>()
                .ForMember(m => m.NotificationTypeId, o => o.MapFrom(s => (int) ZadarmaService.ParseNotificationType(s.Event)))
                .ForMember(m => m.CallStart, o => o.MapFrom(s => s.call_start))
                .ForMember(m => m.PBXCallId, o => o.MapFrom(s => s.pbx_call_id))
                .ForMember(m => m.Internal, o => o.MapFrom(s => s.Internal))
                .ForMember(m => m.Destination, o => o.MapFrom(s => s.destination));

            CreateMap<OutgoingCallEndRequestModel, Call>()
                .ForMember(m => m.NotificationTypeId,
                    o => o.MapFrom(s => (int) ZadarmaService.ParseNotificationType(s.Event)))
                .ForMember(m => m.CallStart, o => o.MapFrom(s => s.call_start))
                .ForMember(m => m.PBXCallId, o => o.MapFrom(s => s.pbx_call_id))
                .ForMember(m => m.Destination, o => o.MapFrom(s => s.destination))
                .ForMember(m => m.Internal, o => o.MapFrom(s => s.Internal))
                .ForMember(m => m.Duration, o => o.MapFrom(s => s.duration))
                .ForMember(m => m.DispositionTypeId, o => o.MapFrom(s => (int) ZadarmaService.ParseDispositionType(s.disposition)))
                .ForMember(m => m.StatusCode, o => o.MapFrom(s => s.status_code))
                .ForMember(m => m.IsRecorded, o => o.MapFrom(s => s.is_recorded))
                .ForMember(m => m.CallIdWithRecord, o => o.MapFrom(s => s.call_id_with_rec));
        }
    }
}