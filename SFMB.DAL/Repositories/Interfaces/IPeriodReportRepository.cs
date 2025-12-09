using SFMB.DAL.Entities;

namespace SFMB.DAL.Repositories.Interfaces
{
    public interface IPeriodReportRepository
    {
        Task<PeriodReport> GetPeriodReportAsync(DateOnly startDate, DateOnly endDate);
        Task<PeriodReport> GetPeriodReportByUserAsync(DateOnly startDate, DateOnly endDate, string userId);
    }
}
