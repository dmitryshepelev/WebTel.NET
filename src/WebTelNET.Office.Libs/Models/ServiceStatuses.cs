namespace WebTelNET.Office.Libs.Models
{
    public enum ServiceStatuses
    {
        Available = 1,
        Activated,
        Unavailable
    }

    public enum ServiceActivationStatus
    {
        ActivationSucceed,
        UnableToActivate,
        RequireAdditionData
    }
}