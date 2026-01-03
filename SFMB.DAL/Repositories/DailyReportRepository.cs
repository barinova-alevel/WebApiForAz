using Microsoft.EntityFrameworkCore;
using SFMB.DAL.Entities;
using SFMB.DAL.Repositories.Interfaces;

namespace SFMB.DAL.Repositories
{
    public class DailyReportRepository : IDailyReportRepository
    {
        private readonly SfmbDbContext _context;

        public DailyReportRepository(SfmbDbContext context)
        {
            _context = context;
        }

        public async Task<DailyReport> GetDailyReportAsync(DateOnly date)
        {

            DateTime now = DateTime.Now;

            var start = date;
            var end = start.AddDays(1);

            var operations = await _context.Operations
                .Include(o => o.OperationType)
                .Where(o => o.Date >= start && o.Date < end)
                .OrderByDescending(o => o.Date)
                .ToListAsync();

            var report = new DailyReport
            {
                Date = date,
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

        public async Task<DailyReport> GetDailyReportByUserAsync(DateOnly date, string userId)
        {
            var start = date;
            var end = start.AddDays(1);

            var operations = await _context.Operations
                .Include(o => o.OperationType)
                .Where(o => o.Date >= start && o.Date < end && o.UserId == userId)
                .OrderByDescending(o => o.Date)
                .ToListAsync();

            var report = new DailyReport
            {
                Date = date,
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
