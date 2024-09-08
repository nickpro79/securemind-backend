using SecureMindAPI.DTOs;
using SecureMindAPI.DTOs.CounsellorRequestDTO;

namespace SecureMindAPI.Contract
{
    public interface ICounsellorsRepository
    {
        public Task<IEnumerable<CounsellerResponseDTO>> GetAll();
        public Task<IEnumerable<CounsellerResponseDTO>> FilterBySpecialization(string specialization);

    }
}
