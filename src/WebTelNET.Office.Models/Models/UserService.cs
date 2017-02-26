using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebTelNET.Office.Models.Models
{
    public class UserService
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public DateTime? ActivationDateTime { get; set; }

        [Required]
        public Guid UserOfficeId { get; set; }
        public UserOffice UserOffice { get; set; }

        [Required]
        public int ServiceStatusId { get; set; }
        public ServiceStatus ServiceStatus { get; set; }

        [Required]
        public Guid ServiceProviderId { get; set; }
        public ServiceProvider ServiceProvider { get; set; }

        public ICollection<UserServiceData> UserServiceData { get; set; }
    }
}