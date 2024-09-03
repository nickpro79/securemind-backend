using AuthenticationAPI.Data;
using AuthenticationAPI.DTO;
using AuthenticationAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthenticationAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _configuration;
        private readonly AuthenticationDbContext _context;
        public UserRepository(IConfiguration configuration, AuthenticationDbContext context)
        {
            _context = context;
            _configuration = configuration;

        }
        public async Task<JwtSecurityToken> GenerateToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            return token;
        }

        public async Task<JwtSecurityToken> Login(LoginDTO loginDTO)
        {
            var roles = new Roles();
            var user = await _context.Users.FirstOrDefaultAsync(x=>x.Name==loginDTO.Name);
            if (user == null || user.Password!=loginDTO.Password)
            {
                var authClaims = new List<Claim>() 
                { 
                new Claim(ClaimTypes.NameIdentifier, loginDTO.Name),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role,roles.User)
                };
                var token = await GenerateToken(authClaims);
                return token;
            }
            return null;
        }

        public async Task<bool> Register(UserDTO userDTO)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x=>x.Name== userDTO.Name||x.Email==userDTO.Email);
            if (user != null)
            {
                return false;
            }

            User newUser = new User()
            {
                Name = userDTO.Name,
                Email = userDTO.Email,
                Password = userDTO.Password,
                RegistrationDate = DateTime.Now,
            };

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
