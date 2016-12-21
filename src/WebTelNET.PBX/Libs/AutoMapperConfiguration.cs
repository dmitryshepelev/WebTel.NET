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
            #region Maps_for_request_models

            CreateMap<IncomingCallStartRequestModel, Call>()
                .ForMember(m => m.NotificationTypeId,
                    o => o.MapFrom(s => (int) ZadarmaService.ParseNotificationType(s.Event)))
                .ForMember(m => m.CallStart, o => o.MapFrom(s => s.call_start))
                .ForMember(m => m.PBXCallId, o => o.MapFrom(s => s.pbx_call_id));

            CreateMap<IncomingCallEndRequestModel, Call>()
                .ForMember(m => m.NotificationTypeId,
                    o => o.MapFrom(s => (int) ZadarmaService.ParseNotificationType(s.Event)))
                .ForMember(m => m.CallStart, o => o.MapFrom(s => s.call_start))
                .ForMember(m => m.PBXCallId, o => o.MapFrom(s => s.pbx_call_id))
                .ForMember(m => m.Internal, o => o.MapFrom(s => s.Internal))
                .ForMember(m => m.Duration, o => o.MapFrom(s => s.duration))
                .ForMember(m => m.DispositionTypeId, o => o.MapFrom(s => (int) ZadarmaService.ParseDispositionType(s.disposition)))
                .ForMember(m => m.StatusCode, o => o.MapFrom(s => s.status_code))
                .ForMember(m => m.IsRecorded, o => o.MapFrom(s => s.is_recorded))
                .ForMember(m => m.CallIdWithRecord, o => o.MapFrom(s => s.call_id_with_rec));

            CreateMap<OutgoingCallStartRequestModel, Call>()
                .ForMember(m => m.NotificationTypeId,
                    o => o.MapFrom(s => (int) ZadarmaService.ParseNotificationType(s.Event)))
                .ForMember(m => m.CallStart, o => o.MapFrom(s => s.call_start))
                .ForMember(m => m.PBXCallId, o => o.MapFrom(s => s.pbx_call_id))
                .ForMember(m => m.Internal, o => o.MapFrom(s => s.Internal))
                .ForMember(m => m.Destination, o => o.Ignore());

            CreateMap<OutgoingCallEndRequestModel, Call>()
                .ForMember(m => m.NotificationTypeId,
                    o => o.MapFrom(s => (int) ZadarmaService.ParseNotificationType(s.Event)))
                .ForMember(m => m.CallStart, o => o.MapFrom(s => s.call_start))
                .ForMember(m => m.PBXCallId, o => o.MapFrom(s => s.pbx_call_id))
                .ForMember(m => m.Internal, o => o.MapFrom(s => s.Internal))
                .ForMember(m => m.Destination, o => o.Ignore())
                .ForMember(m => m.Duration, o => o.MapFrom(s => s.duration))
                .ForMember(m => m.DispositionTypeId, o => o.MapFrom(s => (int) ZadarmaService.ParseDispositionType(s.disposition)))
                .ForMember(m => m.StatusCode, o => o.MapFrom(s => s.status_code))
                .ForMember(m => m.IsRecorded, o => o.MapFrom(s => s.is_recorded))
                .ForMember(m => m.CallIdWithRecord, o => o.MapFrom(s => s.call_id_with_rec));

            #endregion

            #region Maps_for_view_models

            CreateMap<Call, CallViewModel>()
                .ForMember(m => m.CallType, o => o.MapFrom(s => s.NotificationTypeId))
                .ForMember(m => m.Caller, o => o.MapFrom(s => s.Caller != null ? s.Caller.Number : null))
                .ForMember(m => m.Destination, o => o.MapFrom(s => s.Destination != null ? s.Destination.Number : null))
                .ForMember(m => m.Duration, o => o.MapFrom(s => s.Duration))
                .ForMember(m => m.DispositionType, o => o.MapFrom(s => s.DispositionTypeId))
                .ForMember(m => m.IsRecorded, o => o.MapFrom(s => s.IsRecorded));

            #endregion
        }
    }
}