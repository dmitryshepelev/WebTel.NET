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
        private readonly ICallerRepository _callerRepository;
        private readonly ICallRepository _callRepository;

        public ZadarmaManager(
            IMapper mapper,
            ICallerRepository callerRepository,
            ICallRepository callRepository
        )
        {
            _mapper = mapper;
            _callerRepository = callerRepository;
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
                    strategy = new IncomingCallStartNotificationStrategy(_mapper, _callerRepository, _callRepository);
                    break;
                case CallNotificationType.NotifyInternal:
                    strategy = new InternalCallNotificationStrategy(_mapper, _callerRepository, _callRepository);
                    break;
                case CallNotificationType.NotifyEnd:
                    strategy = new IncomingCallEndNotificationStrategy(_mapper, _callerRepository, _callRepository);
                    break;
                case CallNotificationType.NotifyOutStart:
                    strategy = new OutgoingCallStartNotificationStrategy(_mapper, _callerRepository, _callRepository);
                    break;
                case CallNotificationType.NotifyOutEnd:
                    strategy = new OutgoingCallEndNotificationStrategy(_mapper, _callerRepository, _callRepository);
                    break;
                default:
                    throw new NotImplementedException();
            }

            var context = new CallNotificationContext(strategy);
            return context.ProcessNotification(model, zadarmaAccountId);
        }
    }
}
