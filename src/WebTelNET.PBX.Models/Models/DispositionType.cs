using System.ComponentModel.DataAnnotations;

namespace WebTelNET.PBX.Models.Models
{
    public class DispositionType
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}