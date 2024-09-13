using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecureMindAPI.Contract;
using SecureMindAPI.DTOs.CounsellorRequestDTO;

namespace SecureMindAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CounsellorsController : ControllerBase
    {
        private ICounsellorsRepository _counsellorsRepository;

        public CounsellorsController(ICounsellorsRepository counsellorsRepository)
        {
            _counsellorsRepository = counsellorsRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var counsellorList = await _counsellorsRepository.GetAll();
            return Ok(counsellorList);
        }

        
        [HttpGet("Specialization")]
        public async Task<IActionResult> GetBySpecialization([FromQuery] string specialization)
        {
            var counsellorList = await _counsellorsRepository.FilterBySpecialization(specialization);
            return Ok(counsellorList); 
        }

    }
}
