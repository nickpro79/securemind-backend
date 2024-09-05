using AuthenticationAPI.Models;
using System.IdentityModel.Tokens.Jwt;

namespace AuthenticationAPI.Contract
{
    public class ILoginRepository
    {
        public interface ILoginRepostiory
        {
            public Task<JwtSecurityToken> Login(Login model);
        }
    }
}
