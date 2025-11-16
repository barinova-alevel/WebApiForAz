namespace SFMB.BL.Dtos
{
    public class PeriodReportDto
    {
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalExpenses { get; set; }
        public List<OperationDto> Operations { get; set; } = new();
    }
}
