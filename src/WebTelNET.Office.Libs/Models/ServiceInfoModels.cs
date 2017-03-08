using System;

namespace WebTelNET.Office.Libs.Models
{
    public class ServiceInfoResponseModel
    {
        public DateTime? ActivationDateTime { get; set; }
        public int Status { get; set; }
        public string ServiceType { get; set; }
    }

    public class ServiceDataResponseModel
    {
        public string Data { get; set; }
    }
}