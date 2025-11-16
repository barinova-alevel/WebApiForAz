namespace SFMB.BL.Dtos
{
    public class DailyReportDto
    {
        public DateOnly Date { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalExpenses { get; set; }
        public List<OperationDto> Operations { get; set; } = new();
    }
}
