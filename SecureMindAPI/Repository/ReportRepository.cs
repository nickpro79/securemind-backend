using Microsoft.EntityFrameworkCore;
using SecureMindAPI.Contract;
using SecureMindAPI.Data;
using SecureMindAPI.DTOs;

namespace SecureMindAPI.Repository
{
    public class ReportRepository : IReportRepository
    {

        private ApplicationDbContext _context;

        public ReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task<bool> AddReport(ReportDTO report)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ReportDTO>> GetAllReports()
        {
            return await _context.CrimeIncidents
            .Include(i => i.Location) // Include location data
            .Select(i => new ReportDTO
            {
            Description = i.Description,
            ReportTime = i.ReportTime,
            Location = i.Location == null ? null : new LocationDto
            {
            Latitude = i.Location.Latitude,
            Longitude = i.Location.Longitude
            }
            })
            .ToListAsync();
        }
    }
}
