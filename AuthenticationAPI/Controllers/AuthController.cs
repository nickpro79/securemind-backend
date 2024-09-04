using AuthenticationAPI.Contract;
using AuthenticationAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static AuthenticationAPI.Contract.ILoginRepository;
using System.IdentityModel.Tokens.Jwt;

namespace AuthenticationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IRegisterRepository _registerRepository;
        private readonly ILoginRepostiory _repository;

        public AuthController(
            IRegisterRepository registerRepository,
            ILoginRepostiory repostiory)
        {
            _registerRepository = registerRepository;
            _repository = repostiory;
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] Login model)
        {
            var token = await _repository.Login(model);
            if (token != null)
            {
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] Register model)
        {
            var isRegistered = await _registerRepository.Register(model);
            if (isRegistered.Status == "Error")
            {
                return StatusCode(StatusCodes.Status500InternalServerError, isRegistered.Message);
            }
            return Ok(isRegistered.Message);
        }
    }
}
