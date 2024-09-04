using AuthenticationAPI.DTO;
using AuthenticationAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("Register")]
        public IActionResult Register(UserDTO user)
        {
            var registeredUser = _userRepository.Register(user);
            return Ok(registeredUser);
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginDTO login)
        {
            var token = _userRepository.Login(login);
            return Ok(token);
        }
    }
}
