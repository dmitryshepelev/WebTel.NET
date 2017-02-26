using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebTelNET.Office.Models.Models
{
    public class UserServiceData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public Guid UserServiceId { get; set; }
        public UserService UserService { get; set; }

        [Required]
        public string Key { get; set; }

        public string Value { get; set; }
    }
}