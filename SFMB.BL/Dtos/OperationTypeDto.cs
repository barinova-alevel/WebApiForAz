namespace SFMB.BL.Dtos
{
    public class OperationTypeDto
    {
        public int OperationTypeId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool IsIncome { get; set; }
        public virtual ICollection<OperationDto> Operations { get; set; } = new HashSet<OperationDto>();
    }
}
