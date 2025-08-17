using SFMB.DAL.Entities;

namespace SFMB.DAL.Repositories.Interfaces
{
    public interface IPeriodReportRepository
    {
        Task<PeriodReport> GetPeriodReportAsync(DateTime startDate, DateTime endDate);
    }
}
