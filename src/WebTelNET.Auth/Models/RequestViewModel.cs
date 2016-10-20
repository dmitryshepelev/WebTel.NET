using System.ComponentModel.DataAnnotations;

namespace WebTelNET.Auth.Models
{
    public class RequestViewModel
    {
        [Required]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}