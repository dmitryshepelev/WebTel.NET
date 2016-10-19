using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebTelNET.Models.Models
{
    public class AccountRequest
    {
        [Key]
        public string Id { get; set; }

        [Required]
        [MaxLength(24)]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public bool IsComfirmed { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateTime { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [MaxLength(40)]
        public string RequestCode { get; set; }
    }
}