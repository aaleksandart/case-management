using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagement_App.Models
{
    internal class CasesModel
    {
        public string? Header { get; set; }
        public string? Descriptions { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual UserModel? User { get; set; }
        public virtual CaseStateModel? CaseState { get; set; }
        public virtual AdminModel? Admin { get; set; }
    }
}
}
