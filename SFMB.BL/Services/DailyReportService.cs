using Serilog;
using SFMB.BL.Dtos;
using SFMB.BL.Services.Interfaces;
using SFMB.DAL.Repositories.Interfaces;

namespace SFMB.BL.Services
{
    public class DailyReportService : IDailyReportService
    {
        private readonly IDailyReportRepository _dailyReportRepository;
        public DailyReportService(IDailyReportRepository dailyReportRepository)
        {
            _dailyReportRepository = dailyReportRepository;
        }

        public async Task<DailyReportDto> GetDailyReportAsync(DateTime date)
        {
            var report = await _dailyReportRepository.GetDailyReportAsync(date);
            var helper = new DtoMapper();
            var reportDto = helper.DailyReportToDto(report);
            Log.Information($"Generating a daily report for {date}");
            return reportDto;
        }
    }
}
