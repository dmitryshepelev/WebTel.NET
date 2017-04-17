using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebTelNET.PBX.Models.Models
{
    public interface ICallInternal
    {
        string Internal { get; set; }
    }

    public interface ICallEnded
    {
        int? Duration { get; set; }
        int? DispositionTypeId { get; set; }
        DispositionType DispositionType { get; set; }
        string StatusCode { get; set; }
        bool? IsRecorded { get; set; }
        string CallIdWithRecord { get; set; }
    }

    public interface ICallNotification
    {
        int NotificationTypeId { get; set; }
        NotificationType NotificationType { get; set; }
        DateTime CallStart { get; set; }
        string PBXCallId { get; set; }
        Guid? CallerId { get; set; }
        PhoneNumber Caller { get; set; }

        Guid? DestinationId { get; set; }
        PhoneNumber Destination { get; set; }
    }

    public class Call : ICallNotification, ICallInternal, ICallEnded
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public int NotificationTypeId { get; set; }
        public NotificationType NotificationType { get; set; }
        [Required]
        public DateTime CallStart { get; set; }
        [Required]
        public string PBXCallId { get; set; }
        public Guid? CallerId { get; set; }
        public PhoneNumber Caller { get; set; }
        public Guid? DestinationId { get; set; }
        public PhoneNumber Destination { get; set; }

        public string Internal { get; set; }

        public int? Duration { get; set; }
        public int? DispositionTypeId { get; set; }
        public DispositionType DispositionType { get; set; }
        public string StatusCode { get; set; }
        public bool? IsRecorded { get; set; }
        public string CallIdWithRecord { get; set; }
    }
}