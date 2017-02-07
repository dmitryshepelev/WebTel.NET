using System;

namespace WebTelNET.Office.Libs.Models
{
    public class ServiceInfoResponseModel
    {
        public DateTime? ActivationDateTime { get; set; }
        public int Status { get; set; }
        public string ServiceType { get; set; }
    }
}