using System.Collections.Generic;

namespace WebTelNET.PBX.Models.Models
{
    public class ZadarmaAccount
    {
        public string Id { get; set; }

        public string UserKey { get; set; }
        public string SecretKey { get; set; }

        public string UserId { get; set; }

        public ICollection<IncomingCallNotification> IncomingCallNotifications { get; set; }
        public ICollection<OutgoingCallNotification> OutgoingCallNotifications { get; set; }
    }
}
