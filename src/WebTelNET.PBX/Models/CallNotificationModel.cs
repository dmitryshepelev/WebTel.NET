using System;

namespace WebTelNET.PBX.Models
{
//    public interface I

    public static class CallNotificationKind
    {
        public static string NotifyStart => "NOTIFY_START";
        public static string NotifyInternal => "NOTIFY_INTERNAL";
        public static string NotifyEnd => "NOTIFY_END";
        public static string NotifyOutStart => "NOTIFY_OUT_START";
        public static string NotifyOutEnd => "NOTIFY_OUT_END";
    }

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
}