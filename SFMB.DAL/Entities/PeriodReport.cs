namespace SFMB.DAL.Entities
{
    public class PeriodReport
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalExpenses { get; set; }
        public List<Operation> Operations { get; set; } = new();
    }
}
