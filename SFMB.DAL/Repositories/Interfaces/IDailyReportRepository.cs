using SFMB.DAL.Entities;

namespace SFMB.DAL.Repositories.Interfaces
{
    public interface IDailyReportRepository
    {
        Task<DailyReport> GetDailyReportAsync(DateOnly date);
        Task<DailyReport> GetDailyReportByUserAsync(DateOnly date, string userId);
    }
}