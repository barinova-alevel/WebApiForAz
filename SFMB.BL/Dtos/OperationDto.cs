namespace SFMB.BL.Dtos
{
    public class OperationDto
    {
        public int OperationId { get; set; }
        public DateOnly Date { get; set; }
        public decimal Amount { get; set; }
        public string? Note { get; set; }
        public int OperationTypeId { get; set; }
        public OperationTypeDto? OperationType { get; set; }
    }
}
