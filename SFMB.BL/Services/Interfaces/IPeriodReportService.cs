using SFMB.BL.Dtos;

namespace SFMB.BL.Services.Interfaces
{
    public interface IPeriodReportService
    {
        Task<PeriodReportDto> GetPeriodReportAsync(DateTime startDate, DateTime endDate);
    }
}
