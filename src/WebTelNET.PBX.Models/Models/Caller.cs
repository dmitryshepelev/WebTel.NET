using System.ComponentModel.DataAnnotations;

namespace WebTelNET.PBX.Models.Models
{
    public class Caller
    {
        public string Id { get; set; }
        [Required]
        public string Number { get; set; }
        public string Description { get; set; }
    }
}