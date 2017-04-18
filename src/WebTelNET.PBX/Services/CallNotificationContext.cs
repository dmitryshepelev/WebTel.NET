using System;
using AutoMapper;
using Newtonsoft.Json.Linq;
using WebTelNET.PBX.Models.Models;
using WebTelNET.PBX.Models.Repository;

namespace WebTelNET.PBX.Services
{
    public abstract class CallNotificationStrategy
    {
        protected readonly IMapper Mapper;
        protected readonly IPhoneNumberRepository PhoneNumberRepository;
        protected readonly ICallRepository CallRepository;

        protected CallNotificationStrategy(
            IMapper mapper,
            IPhoneNumberRepository phoneNumberRepository,
            ICallRepository callRepository
        )
        {
            Mapper = mapper;
            PhoneNumberRepository = phoneNumberRepository;
            CallRepository = callRepository;
        }

        public abstract Call Process(JObject model, Guid zadarmaAccountId);
    }

    public class IncomingCallStartNotificationStrategy : CallNotificationStrategy
    {
        public IncomingCallStartNotificationStrategy(
            IMapper mapper,
            IPhoneNumberRepository phoneNumberRepository,
            ICallRepository callRepository
        ) : base(mapper, phoneNumberRepository, callRepository)
        {

        }

        public override Call Process(JObject model, Guid zadarmaAccountId)
        {
            var requestModel = model.ToObject<IncomingCallStartRequestModel>();
            var callerPhoneNumber =
                PhoneNumberRepository.GetOrCreate(new PhoneNumber
                {
                    Number = requestModel.caller_id,
                    ZadarmaAccountId = zadarmaAccountId
                });

            var destinationPhoneNumber =
                PhoneNumberRepository.GetOrCreate(new PhoneNumber
                {
                    Number = requestModel.called_did,
                    ZadarmaAccountId = zadarmaAccountId
                });

            var mapped = Mapper.Map<Call>(requestModel);
            mapped.CallerId = callerPhoneNumber.Id;
            mapped.DestinationId = destinationPhoneNumber.Id;

            return CallRepository.Create(mapped);
        }
    }

    public class InternalCallNotificationStrategy : CallNotificationStrategy
    {
        public InternalCallNotificationStrategy(
            IMapper mapper,
            IPhoneNumberRepository phoneNumberRepository,
            ICallRepository callRepository
        ) : base(mapper, phoneNumberRepository, callRepository)
        {
        }

        public override Call Process(JObject model, Guid zadarmaAccountId)
        {
            throw new NotImplementedException();
        }
    }

    public class IncomingCallEndNotificationStrategy : CallNotificationStrategy
    {
        public IncomingCallEndNotificationStrategy(
            IMapper mapper,
            IPhoneNumberRepository phoneNumberRepository,
            ICallRepository callRepository
        ) : base(mapper, phoneNumberRepository, callRepository)
        {
        }

        public override Call Process(JObject model, Guid zadarmaAccountId)
        {
            var requestModel = model.ToObject<IncomingCallEndRequestModel>();

            var mapped = Mapper.Map<Call>(requestModel);

            var incomingCall =
                CallRepository.GetSingle(
                    x =>
                        x.PBXCallId.Equals(mapped.PBXCallId) &&
                        x.NotificationTypeId == (int) CallNotificationType.NotifyStart);

            if (incomingCall == null)
            {
                throw new NullReferenceException("Call doesn't exist.");
            }

            incomingCall.NotificationTypeId = mapped.NotificationTypeId;
            incomingCall.Internal = mapped.Internal;
            incomingCall.Duration = mapped.Duration;
            incomingCall.DispositionTypeId = mapped.DispositionTypeId;
            incomingCall.StatusCode = mapped.StatusCode;
            incomingCall.IsRecorded = mapped.IsRecorded;
            incomingCall.CallIdWithRecord = mapped.CallIdWithRecord;

            return CallRepository.Update(incomingCall);
        }
    }

    public class OutgoingCallStartNotificationStrategy : CallNotificationStrategy
    {
        public OutgoingCallStartNotificationStrategy(
            IMapper mapper,
            IPhoneNumberRepository phoneNumberRepository,
            ICallRepository callRepository
        ) : base(mapper, phoneNumberRepository, callRepository)
        {
        }

        public override Call Process(JObject model, Guid zadarmaAccountId)
        {
            var requestModel = model.ToObject<OutgoingCallStartRequestModel>();

            var mapped = Mapper.Map<Call>(requestModel);
            return CallRepository.Create(mapped);
        }
    }

    public class OutgoingCallEndNotificationStrategy : CallNotificationStrategy
    {
        public OutgoingCallEndNotificationStrategy(
            IMapper mapper,
            IPhoneNumberRepository phoneNumberRepository,
            ICallRepository callRepository
        ) : base(mapper, phoneNumberRepository, callRepository)
        {
        }

        public override Call Process(JObject model, Guid zadarmaAccountId)
        {
            var requestModel = model.ToObject<OutgoingCallEndRequestModel>();

            var mapped = Mapper.Map<Call>(requestModel);

            var outgoingCall =
                CallRepository.GetSingle(
                    x =>
                        x.PBXCallId.Equals(mapped.PBXCallId) &&
                        x.NotificationTypeId == (int) CallNotificationType.NotifyOutStart);

            if (outgoingCall == null)
            {
                throw new NullReferenceException();
            }

            outgoingCall.NotificationTypeId = mapped.NotificationTypeId;
            outgoingCall.Internal = mapped.Internal;
            outgoingCall.Duration = mapped.Duration;
            outgoingCall.DispositionTypeId = mapped.DispositionTypeId;
            outgoingCall.StatusCode = mapped.StatusCode;
            outgoingCall.IsRecorded = mapped.IsRecorded;
            outgoingCall.CallIdWithRecord = mapped.CallIdWithRecord;

            var callerPhoneNumber =
                PhoneNumberRepository.GetOrCreate(new PhoneNumber
                {
                    Number = requestModel.caller_id,
                    ZadarmaAccountId = zadarmaAccountId
                });

            var destinationPhoneNumber =
                PhoneNumberRepository.GetOrCreate(new PhoneNumber
                {
                    Number = requestModel.destination,
                    ZadarmaAccountId = zadarmaAccountId
                });
            outgoingCall.CallerId = callerPhoneNumber.Id;
            outgoingCall.DestinationId = destinationPhoneNumber.Id;

            return CallRepository.Update(outgoingCall);
        }
    }

    public class CallNotificationContext
    {
        private readonly CallNotificationStrategy _stategy;

        public CallNotificationContext(CallNotificationStrategy strategy)
        {
            _stategy = strategy;
        }

        public Call ProcessNotification(CallRequestModelHeap model, Guid zadarmaAccountId)
        {
            return _stategy.Process(JObject.FromObject(model), zadarmaAccountId);
        }
    }
}