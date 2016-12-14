﻿using System;
using System.ComponentModel.DataAnnotations;
using Org.BouncyCastle.Ocsp;

namespace WebTelNET.PBX.Models.Models
{
    public interface ICallInternal
    {
        string Internal { get; set; }
    }

    public interface ICallEnded
    {
        int Duration { get; set; }

        int DispositionTypeId { get; set; }
        DispositionType DispositionType { get; set; }
        string StatusCode { get; set; }
        bool IsRecorded { get; set; }
        string CallIdWithRecord { get; set; }
    }

    public interface ICallNotification
    {
        string Id { get; set; }

        [Required]
        int NotificationTypeId { get; set; }
        NotificationType NotificationType { get; set; }
        [Required]
        DateTime CallStart { get; set; }
        [Required]
        string PBXCallId { get; set; }
    }

    public class OutgoingCallNotification : ICallNotification, ICallInternal, ICallEnded
    {
        public string Id { get; set; }
        public string CallerId { get; set; }
        [Required]
        public string Destination { get; set; }
        public int NotificationTypeId { get; set; }
        public NotificationType NotificationType { get; set; }
        public DateTime CallStart { get; set; }
        public string PBXCallId { get; set; }
        public string Internal { get; set; }
        public int Duration { get; set; }
        public int DispositionTypeId { get; set; }
        public DispositionType DispositionType { get; set; }
        public string StatusCode { get; set; }
        public bool IsRecorded { get; set; }
        public string CallIdWithRecord { get; set; }
    }
    
    public class IncomingCallNotification : ICallNotification, ICallInternal, ICallEnded
    {
        public string Id { get; set; }
        [Required]
        public string CallerId { get; set; }
        public Caller Caller { get; set; }
        [Required]
        public string CalledDid { get; set; }
        public int NotificationTypeId { get; set; }
        public NotificationType NotificationType { get; set; }
        public DateTime CallStart { get; set; }
        public string PBXCallId { get; set; }
        public string Internal { get; set; }
        public int Duration { get; set; }
        public int DispositionTypeId { get; set; }
        public DispositionType DispositionType { get; set; }
        public string StatusCode { get; set; }
        public bool IsRecorded { get; set; }
        public string CallIdWithRecord { get; set; }
    }
}