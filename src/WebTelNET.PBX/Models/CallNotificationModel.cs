using System;

namespace WebTelNET.PBX.Models
{
    public class CallNotificationModel
    {
        public string Event { get; set; }
        public DateTime call_start { get; set; }
        public string pbx_call_id { get; set; }
    }

    public class IncomingCallStartNotificationModel : CallNotificationModel
    {
        public string caller_id { get; set; }
        public string called_did { get; set; }
    }

    public abstract class CallNotificationViewModel
    {
        public int NotificationType { get; set; }
        public DateTime CallStart { get; set; }
        public string PBXCallId { get; set; }
    }

    public class IncomingCallNotificationViewModel : CallNotificationViewModel
    {
        public string CallerId { get; set; }
        public string CalledDid { get; set; }
    }
}