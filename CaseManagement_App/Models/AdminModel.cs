using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagement_App.Models
{
    internal class AdminModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public virtual RoleModel? Role { get; set; }
    }
}
