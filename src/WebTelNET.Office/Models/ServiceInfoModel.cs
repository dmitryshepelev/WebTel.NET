using System;

namespace WebTelNET.Office.Models
{
    public class ServiceInfoRequestModel
    {
        public string ServiceTypeName { get; set; }
    }

    public class ServiceInfoResponseModel
    {
        public DateTime? ActivationDateTime { get; set; }
        public int Status { get; set; }
        public string ServiceType { get; set; }
    }
}