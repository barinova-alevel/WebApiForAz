using Azure;

namespace SFMB.DAL.Entities
{
    public class DailyReport
    {
        public DateOnly Date { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalExpenses { get; set; }
        public List<Operation> Operations { get; set; } = new();
    }
}
