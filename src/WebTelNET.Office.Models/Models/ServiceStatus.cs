using System.ComponentModel.DataAnnotations;

namespace WebTelNET.Office.Models.Models
{
    public class ServiceStatus
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}