using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagement_App.Entities
{
    [Index(nameof(Name), IsUnique = true)]
    internal class Role
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string? Name { get; set; }


        public virtual ICollection<User>? Users { get; set; } = new List<User>();
        public virtual ICollection<Admin>? Admin { get; set; } = new List<User>();
    }
}
