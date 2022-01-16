using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagement_App.Entities
{
    internal class Address
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string? StreetName { get; set; }

        [Required]
        [StringLength(50)]
        public string? PostalCode { get; set; }

        [Required]
        [StringLength(50)]
        public string? City { get; set; }

        [Required]
        [StringLength(50)]
        public string? Country { get; set; }

        
        public virtual ICollection<User>? Users { get; set; } = new List<User>();
    }
}
