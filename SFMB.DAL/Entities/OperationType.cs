using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMB.DAL.Entities
{
    public class OperationType
    {
        public int OperationTypeId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool IsIncome { get; set; }
       
        public virtual ICollection<Operation> Operations { get; set; } = new HashSet<Operation>();
        
        // User ownership
        public string UserId { get; set; } = string.Empty;
        public virtual ApplicationUser? User { get; set; }
    }
}
