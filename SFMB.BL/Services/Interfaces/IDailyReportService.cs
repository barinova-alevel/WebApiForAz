using SFMB.BL.Dtos;

namespace SFMB.BL.Services.Interfaces
{
    public interface IDailyReportService
    {
        Task<DailyReportDto> GetDailyReportAsync(DateTime date);
    }
}
