using Microsoft.AspNetCore.Mvc;
using SFMB.BL.Dtos;
using SFMB.BL.Services.Interfaces;

namespace WebApiForAz.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeriodReportController : ControllerBase
    {
        private readonly IPeriodReportService _periodReportService;

        public PeriodReportController(IPeriodReportService periodReportService)
        {
            _periodReportService = periodReportService;
        }

        [HttpGet("report/period")]
        public async Task<ActionResult<PeriodReportDto>> GetPeriodReport(
            [FromQuery] DateOnly startDate,
            [FromQuery] DateOnly endDate)
        {
            if (startDate > endDate)
            {
                return BadRequest("Start date must be less than or equal to end date.");
            }

            var report = await _periodReportService.GetPeriodReportAsync(startDate, endDate);
            return Ok(report);
        }
    }
}
