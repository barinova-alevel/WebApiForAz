using System.Text.Json.Serialization;

namespace SFMB.DAL.Entities
{
    public class Operation
    {
        public int OperationId { get; set; }
        public DateOnly Date { get; set; }
        public decimal Amount { get; set; }
        public string? Note { get; set; }
        public int OperationTypeId { get; set; }
        
        [JsonIgnore]
        public virtual OperationType? OperationType { get; set; }
        
        // User ownership
        public string UserId { get; set; } = string.Empty;
        public virtual ApplicationUser? User { get; set; }
    }
}
