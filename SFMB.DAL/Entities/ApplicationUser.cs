using Microsoft.AspNetCore.Identity;

namespace SFMB.DAL.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        // Navigation property for user's operations
        public virtual ICollection<Operation> Operations { get; set; } = new HashSet<Operation>();
        public virtual ICollection<OperationType> OperationTypes { get; set; } = new HashSet<OperationType>();
    }
}
