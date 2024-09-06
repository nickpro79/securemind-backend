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
            var incidents = await _context.CrimeIncidents
                 .Include(i => i.Location)
                 .Select(i => new IncidentsDto
                 {
                     IncidentId = i.IncidentId,
                     Description = i.Description,
                     ReportTime = i.ReportTime,
                     LocationId = i.Location.LocationId
                 })
                 .ToListAsync();

            return incidents;
        }
    }
}
