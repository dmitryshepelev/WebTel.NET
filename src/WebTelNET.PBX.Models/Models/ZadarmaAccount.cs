using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebTelNET.PBX.Models.Models
{
    public class ZadarmaAccount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string UserKey { get; set; }
        public string SecretKey { get; set; }

        public string UserId { get; set; }

        public ICollection<IncomingCallNotification> IncomingCallNotifications { get; set; }
        public ICollection<OutgoingCallNotification> OutgoingCallNotifications { get; set; }
    }
}
