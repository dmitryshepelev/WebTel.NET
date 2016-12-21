using System;

namespace WebTelNET.PBX.Models
{
    public class CallViewModel
    {
        public int CallType { get; set; }
        public DateTime CallStart { get; set; }
        public string PBXCallId { get; set; }
        public string Caller { get; set; }
        public string Destination { get; set; }
        public string Internal { get; set; }
        public int? Duration { get; set; }
        public int? DispositionType { get; set; }
        public string StatusCode { get; set; }
        public bool? IsRecorded { get; set; }
        public string CallIdWithRecord { get; set; }
    }
}