using Microsoft.EntityFrameworkCore;
using SecureMindAPI.Contract;
using SecureMindAPI.Data;
using SecureMindAPI.DTOs;
using SecureMindAPI.Models;

namespace SecureMindAPI.Repository
{
    public class CrimeIncidentRepository : ICrimeIncident
    {
        private readonly ApplicationDbContext _context;
        public CrimeIncidentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<IncidentsDto>> GetAllIncidentsAsync()
        {
            return await _context.CrimeIncidents
                .Include(i => i.Location) // Include location data
                .Select(i => new IncidentsDto
                {
                    IncidentId = i.IncidentId,
                    Description = i.Description,
                    ReportTime = i.ReportTime,
                    Location = i.Location == null ? null : new LocationDto
                    {
                        Latitude = i.Location.Latitude,
                        Longitude = i.Location.Longitude,
                        LocationName=i.Location.Name
                    }
                })
                .ToListAsync();
        }
    }
}
