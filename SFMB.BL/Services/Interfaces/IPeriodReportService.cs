using SFMB.BL.Dtos;

namespace SFMB.BL.Services.Interfaces
{
    public interface IPeriodReportService
    {
        Task<PeriodReportDto> GetPeriodReportAsync(DateOnly startDate, DateOnly endDate);
    }
}
