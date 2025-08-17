namespace SFMB.DAL.Entities
{
    public class Operation
    {
        public int OperationId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string? Note { get; set; }
        public int OperationTypeId { get; set; }
        public virtual OperationType? OperationType { get; set; }
    }
}
