using System.ComponentModel.DataAnnotations;

namespace WebTelNET.Auth.Models
{
    public class SignUpViewModel
    {
        [Required]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}