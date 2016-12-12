using WebTelNET.PBX.Models;

namespace WebTelNET.PBX.Services
{
    public abstract class CallNotificationStrategy
    {

    }

    public class IncomingClassStartNotificationStrategy : CallNotificationStrategy
    {
//        private

        public IncomingClassStartNotificationStrategy()
        {
        }
    }

    public class NotifyCallContext
    {
        private CallNotificationStrategy _stategy;
        private CallNotificationModel _model;

        public NotifyCallContext(CallNotificationModel model)
        {
            _model = model;
            if (model.Event == CallNotificationKind.NotifyStart)
            {
                _stategy = new IncomingClassStartNotificationStrategy();
            }
        }

        public void ApplyCall()
        {
//            _stategy;
        }
    }
}