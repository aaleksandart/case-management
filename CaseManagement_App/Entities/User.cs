using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagement_App.Entities
{
    [Index(nameof(ContactInfoId), IsUnique = true)]
    internal class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string? FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string? LastName { get; set; }


        [Required]
        public int RoleId { get; set; }
        public Role? Role { get; set; }

        [Required]
        public int ContactInfoId { get; set; }
        public ContactInfo? ContactInfo { get; set; }

        [Required]
        public int AddressId { get; set; }
        public Address? Address { get; set; }
    }
}
