using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebTelNET.Office.Models.Models
{
    public class ServiceStatus
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}