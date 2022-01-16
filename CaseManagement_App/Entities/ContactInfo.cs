using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagement_App.Entities
{
    [Index(nameof(Email), IsUnique = true)]
    internal class ContactInfo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? Email { get; set; }

        [Required]
        public int PhoneNumber { get; set; }


        public virtual IEnumerable<User> Users { get; set; } = new List<User>();
    }
}
