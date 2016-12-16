using System;

namespace WebTelNET.PBX.Models
{
    public abstract class CallViewModel
    {
        public int NotificationType { get; set; }
        public DateTime CallStart { get; set; }
        public string PBXCallId { get; set; }
    }

    public class IncomingCallViewModel : CallViewModel
    {
        public string CallerId { get; set; }
        public string Destination { get; set; }
    }
}