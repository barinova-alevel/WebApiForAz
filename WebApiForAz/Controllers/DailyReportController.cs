using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SFMB.BL.Dtos;
using SFMB.BL.Services.Interfaces;
using SFMB.DAL.Repositories.Interfaces;

namespace WebApiForAz.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DailyReportController : ControllerBase
    {
        private readonly IDailyReportService _dailyReportService;
        //private readonly IDailyReportRepository _dailyReportRepository;

       // public DailyReportController(IDailyReportService dailyReportService, IDailyReportRepository dailyReportRepository)
        public DailyReportController(IDailyReportService dailyReportService)
        {
            _dailyReportService = dailyReportService;
           // _dailyReportRepository = dailyReportRepository;
        }

        private string GetCurrentUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        }

        private bool IsAdmin()
        {
            return User.IsInRole("Admin");
        }

        [HttpGet("report/daily")]
        public async Task<ActionResult<DailyReportDto>> GetDailyReport([FromQuery] DateOnly date)
        {
            var userId = GetCurrentUserId();
            
            if (IsAdmin())
            {
                //var report = await _dailyReportRepository.GetDailyReportAsync(date);
                var report = await _dailyReportService.GetDailyReportAsync(date);
                return Ok(report);
            }
            else
            {
                var report = await _dailyReportService.GetDailyReportByUserAsync(date, userId);
                return Ok(report);
            }
        }
    }
}
