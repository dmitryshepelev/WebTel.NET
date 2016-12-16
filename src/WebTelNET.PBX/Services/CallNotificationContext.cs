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
        protected readonly ICallerRepository CallerRepository;
        protected readonly ICallRepository CallRepository;

        protected CallNotificationStrategy(
            IMapper mapper,
            ICallerRepository callerRepository,
            ICallRepository callRepository
        )
        {
            Mapper = mapper;
            CallerRepository = callerRepository;
            CallRepository = callRepository;
        }

        public abstract Call Process(JObject model, Guid zadarmaAccountId);
    }

    public class IncomingCallStartNotificationStrategy : CallNotificationStrategy
    {
        public IncomingCallStartNotificationStrategy(
            IMapper mapper,
            ICallerRepository callerRepository,
            ICallRepository callRepository
        ) : base(mapper, callerRepository, callRepository)
        {

        }

        public override Call Process(JObject model, Guid zadarmaAccountId)
        {
            var requestModel = model.ToObject<IncomingCallStartRequestModel>();
            var caller =
                CallerRepository.GetOrCreate(new Caller
                {
                    Number = requestModel.caller_id,
                    ZadarmaAccountId = zadarmaAccountId
                });

            var mapped = Mapper.Map<Call>(requestModel);
            mapped.CallerId = caller.Id;

            return CallRepository.Create(mapped);
        }
    }

    public class InternalCallNotificationStrategy : CallNotificationStrategy
    {
        public InternalCallNotificationStrategy(
            IMapper mapper,
            ICallerRepository callerRepository,
            ICallRepository callRepository
        ) : base(mapper, callerRepository, callRepository)
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
            ICallerRepository callerRepository,
            ICallRepository callRepository
        ) : base(mapper, callerRepository, callRepository)
        {
        }

        public override Call Process(JObject model, Guid zadarmaAccountId)
        {
            throw new NotImplementedException();
        }
    }

    public class OutgoingCallStartNotificationStrategy : CallNotificationStrategy
    {
        public OutgoingCallStartNotificationStrategy(
            IMapper mapper,
            ICallerRepository callerRepository,
            ICallRepository callRepository
        ) : base(mapper, callerRepository, callRepository)
        {
        }

        public override Call Process(JObject model, Guid zadarmaAccountId)
        {
            throw new NotImplementedException();
        }
    }

    public class OutgoingCallEndNotificationStrategy : CallNotificationStrategy
    {
        public OutgoingCallEndNotificationStrategy(
            IMapper mapper,
            ICallerRepository callerRepository,
            ICallRepository callRepository
        ) : base(mapper, callerRepository, callRepository)
        {
        }

        public override Call Process(JObject model, Guid zadarmaAccountId)
        {
            throw new NotImplementedException();
        }
    }

    public class CallNotificationContext
    {
        private readonly CallNotificationStrategy _stategy;

        public CallNotificationContext(CallNotificationStrategy strategy)
        {
            _stategy = strategy;
        }

        public Call ProcessNotification(JObject model, Guid zadarmaAccountId)
        {
            return _stategy.Process(model, zadarmaAccountId);
        }
    }
}