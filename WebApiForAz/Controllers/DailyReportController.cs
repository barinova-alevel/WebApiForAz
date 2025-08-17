using Microsoft.AspNetCore.Mvc;
using SFMB.BL.Dtos;
using SFMB.BL.Services.Interfaces;

namespace WebApiForAz.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class DailyReportController : ControllerBase
    {
        private readonly IDailyReportService _dailyReportService;

        public DailyReportController(IDailyReportService dailyReportService)
        {
            _dailyReportService = dailyReportService;
        }

        [HttpGet("report/daily")]
        public async Task<ActionResult<DailyReportDto>> GetDailyReport([FromQuery] DateTime date)
        {
            var report = await _dailyReportService.GetDailyReportAsync(date);
            return Ok(report);
        }
    }
}
