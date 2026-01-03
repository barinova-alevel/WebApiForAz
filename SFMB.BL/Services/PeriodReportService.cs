using Serilog;
using SFMB.BL.Dtos;
using SFMB.BL.Services.Interfaces;
using SFMB.DAL.Repositories.Interfaces;

namespace SFMB.BL.Services
{
    public class PeriodReportService : IPeriodReportService
    {
        private readonly IPeriodReportRepository _periodReportRepository;

        public PeriodReportService(IPeriodReportRepository periodReportRepository)
        {
            _periodReportRepository = periodReportRepository;
        }

        public async Task<PeriodReportDto> GetPeriodReportAsync(DateOnly startDate, DateOnly endDate)
        {
            var report = await _periodReportRepository.GetPeriodReportAsync(startDate, endDate);
            var helper = new DtoMapper();
            var reportDto = helper.PeriodReportToDto(report);
            Log.Information($"Generating a daily report for period from {startDate} to {endDate}");
            return reportDto;
        }

        public async Task<PeriodReportDto> GetPeriodReportByUserAsync(DateOnly startDate, DateOnly endDate, string userId)
        {
            var report = await _periodReportRepository.GetPeriodReportByUserAsync(startDate, endDate, userId);
            var helper = new DtoMapper();
            var reportDto = helper.PeriodReportToDto(report);
            Log.Information($"Generating a daily report for period from {startDate} to {endDate}");
            return reportDto;
        }
    }
}
