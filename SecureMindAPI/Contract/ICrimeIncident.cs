using SecureMindAPI.DTOs;

namespace SecureMindAPI.Contract
{
    public interface ICrimeIncident
    {
        Task<IEnumerable<IncidentsDto>> GetAllIncidentsAsync();
    }
}
