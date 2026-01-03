using SFMB.BL.Dtos;

namespace SFMB.BL.Services.Interfaces
{
    public interface IPeriodReportService
    {
        Task<PeriodReportDto> GetPeriodReportAsync(DateOnly startDate, DateOnly endDate);

        Task<PeriodReportDto> GetPeriodReportByUserAsync(DateOnly startDate, DateOnly endDate, string userId);
    }
}
