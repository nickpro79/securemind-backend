using SecureMindAPI.DTOs;

namespace SecureMindAPI.Contract
{
    public interface ICounsellorsRepository
    {
        public Task<IEnumerable<CounsellerDTO>> GetAll();
        public Task<IEnumerable<CounsellerDTO>> FilterBySpecialization(string specialization);

    }
}
