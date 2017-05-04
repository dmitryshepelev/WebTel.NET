namespace WebTelNET.Office.Libs.Models
{
    public enum ServiceStatuses
    {
        Available = 1,
        Activated,
        Unavailable
    }

    public enum ServiceOperationStatus
    {
        ActivationSucceed,
        UnableToActivate,
        RequireAdditionData,
        EditionSucceed,
        UnableToPerfomOperation
    }
}