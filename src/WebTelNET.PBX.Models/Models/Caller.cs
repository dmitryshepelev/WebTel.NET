using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebTelNET.PBX.Models.Models
{
    public class Caller
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        public string Number { get; set; }
        public string Description { get; set; }

        [Required]
        public Guid ZadarmaAccountId { get; set; }
        public ZadarmaAccount ZadarmaAccount { get; set; }

        public ICollection<Call> Calls { get; set; }
    }
}