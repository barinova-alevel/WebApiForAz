using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SFMB.BL.Dtos;
using SFMB.BL.Services.Interfaces;
using SFMB.DAL.Repositories.Interfaces;

namespace WebApiForAz.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PeriodReportController : ControllerBase
    {
        private readonly IPeriodReportService _periodReportService;
        private readonly IPeriodReportRepository _periodReportRepository;

        public PeriodReportController(IPeriodReportService periodReportService, IPeriodReportRepository periodReportRepository)
        {
            _periodReportService = periodReportService;
            _periodReportRepository = periodReportRepository;
        }

        private string GetCurrentUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        }

        private bool IsAdmin()
        {
            return User.IsInRole("Admin");
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

            var userId = GetCurrentUserId();
            
            if (IsAdmin())
            {
                var report = await _periodReportRepository.GetPeriodReportAsync(startDate, endDate);
                return Ok(report);
            }
            else
            {
                var report = await _periodReportRepository.GetPeriodReportByUserAsync(startDate, endDate, userId);
                return Ok(report);
            }
        }
    }
}
