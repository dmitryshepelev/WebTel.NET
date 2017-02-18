namespace WebTelNET.PBX.Models
{
    public class NotificationConfigModel
    {
        public bool IsConfigured { get; set; }
    }

    public class NotificationConfigInfo : NotificationConfigModel
    {
        public string Link { get; set; }
    }
}