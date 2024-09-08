using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecureMindAPI.Contract;
using SecureMindAPI.DTOs.CounsellorRequestDTO;

namespace SecureMindAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<IActionResult> GetBySpecilization([FromBody] CounsellorRequestBySpecialization request)
        {
            var counsellerList = await _counsellorsRepository.FilterBySpecialization(request.Specialization);
            return Ok(counsellerList);
        }

    }
}
