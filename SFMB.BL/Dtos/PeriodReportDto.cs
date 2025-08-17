namespace SFMB.BL.Dtos
{
    public class PeriodReportDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalExpenses { get; set; }
        public List<OperationDto> Operations { get; set; } = new();
    }
}
