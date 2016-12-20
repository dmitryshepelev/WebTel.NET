using System;
using AutoMapper;
using Newtonsoft.Json.Linq;
using WebTelNET.PBX.Models.Models;
using WebTelNET.PBX.Models.Repository;

namespace WebTelNET.PBX.Services
{
    public class ZadarmaManager : IPBXManager
    {
        private readonly IMapper _mapper;
        private readonly IPhoneNumberRepository _phoneNumberRepository;
        private readonly ICallRepository _callRepository;

        public ZadarmaManager(
            IMapper mapper,
            IPhoneNumberRepository phoneNumberRepository,
            ICallRepository callRepository
        )
        {
            _mapper = mapper;
            _phoneNumberRepository = phoneNumberRepository;
            _callRepository = callRepository;
        }

        public Call ProcessCallNotification(JObject model, Guid zadarmaAccountId)
        {
            var baseModel = model.ToObject<CallRequestModel>();
            var notificationType = ZadarmaService.ParseNotificationType(baseModel.Event);

            CallNotificationStrategy strategy;
            switch (notificationType)
            {
                case CallNotificationType.NotifyStart:
                    strategy = new IncomingCallStartNotificationStrategy(_mapper, _phoneNumberRepository, _callRepository);
                    break;
                case CallNotificationType.NotifyInternal:
                    strategy = new InternalCallNotificationStrategy(_mapper, _phoneNumberRepository, _callRepository);
                    break;
                case CallNotificationType.NotifyEnd:
                    strategy = new IncomingCallEndNotificationStrategy(_mapper, _phoneNumberRepository, _callRepository);
                    break;
                case CallNotificationType.NotifyOutStart:
                    strategy = new OutgoingCallStartNotificationStrategy(_mapper, _phoneNumberRepository, _callRepository);
                    break;
                case CallNotificationType.NotifyOutEnd:
                    strategy = new OutgoingCallEndNotificationStrategy(_mapper, _phoneNumberRepository, _callRepository);
                    break;
                default:
                    throw new NotImplementedException();
            }

            var context = new CallNotificationContext(strategy);
            return context.ProcessNotification(model, zadarmaAccountId);
        }
    }
}
