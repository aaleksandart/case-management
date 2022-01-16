using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagement_App.Entities
{
    internal class Cases
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Header { get; set; }

        [Required]
        public string? Descriptions { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }


        [Required]
        public int UserId { get; set; }
        public virtual User? User { get; set; }

        [Required]
        public int CaseStateId { get; set; }
        public virtual CaseState? CaseState { get; set; }

        
        public int AdminId { get; set; }
        public virtual Admin? Admin { get; set; }
    }
}
