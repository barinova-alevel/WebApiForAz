using Microsoft.EntityFrameworkCore;
using SFMB.DAL.Entities;
using SFMB.DAL.Repositories.Interfaces;

namespace SFMB.DAL.Repositories
{
    public class PeriodReportRepository : IPeriodReportRepository
    {
        private readonly SfmbDbContext _context;

        public PeriodReportRepository(SfmbDbContext context)
        {
            _context = context;
        }

        public async Task<PeriodReport> GetPeriodReportAsync(DateOnly startDate, DateOnly endDate)
        {
            var start = startDate;
            var end = endDate.AddDays(1);

            var operations = await _context.Operations
                .Include(o => o.OperationType)
                .Where(o => o.Date >= start && o.Date < end)
                .ToListAsync();

            var report = new PeriodReport
            {
                StartDate = startDate,
                EndDate = endDate,
                TotalIncome = operations
                    .Where(o => o.OperationType != null && o.OperationType.IsIncome)
                    .Sum(o => o.Amount),
                TotalExpenses = operations
                    .Where(o => o.OperationType != null && !o.OperationType.IsIncome)
                    .Sum(o => o.Amount),
                Operations = operations
            };
            return report;
        }
    }
}
