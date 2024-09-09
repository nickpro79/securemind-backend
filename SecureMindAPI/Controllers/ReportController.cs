using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecureMindAPI.Contract;
using SecureMindAPI.DTOs;

namespace SecureMindAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private IReportRepository _reportRepository;
        public ReportController(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReports()
        {
            var reportList=await _reportRepository.GetAllReports();
            return Ok(reportList);
        }
        [HttpPost]
        public async Task<IActionResult> AddReport(ReportDTO report)
        {
            var reportAdded = await _reportRepository.AddReport(report);
            return Ok(reportAdded);
        }
    }
}
