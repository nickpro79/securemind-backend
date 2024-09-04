using AuthenticationAPI.Models;

namespace AuthenticationAPI.Contract
{
    public interface IRegisterRepository
    {
        public Task<Response> Register(Register model);
    }
}
