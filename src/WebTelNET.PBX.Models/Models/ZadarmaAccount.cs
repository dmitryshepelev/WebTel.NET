using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebTelNET.PBX.Models.Models
{
    public class ZadarmaAccount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public string UserKey { get; set; }
        [Required]
        public string SecretKey { get; set; }

        [Required]
        public string UserId { get; set; }

        public ICollection<PhoneNumber> PhoneNumbers { get; set; }
    }
}
