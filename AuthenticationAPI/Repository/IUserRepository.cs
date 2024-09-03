using AuthenticationAPI.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AuthenticationAPI.Repository
{
    public interface IUserRepository
    {
        public Task<JwtSecurityToken> Login(LoginDTO loginDTO);
        public Task<bool> Register(UserDTO userDTO);
        public Task<JwtSecurityToken> GenerateToken(List<Claim> authClaims);
    }
}
