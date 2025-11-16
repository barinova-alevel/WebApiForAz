namespace SFMB.DAL.Entities
{
    public class PeriodReport
    {
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalExpenses { get; set; }
        public List<Operation> Operations { get; set; } = new();
    }
}
