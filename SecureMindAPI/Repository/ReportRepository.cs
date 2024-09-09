using Microsoft.EntityFrameworkCore;
using SecureMindAPI.Contract;
using SecureMindAPI.Data;
using SecureMindAPI.DTOs;
using SecureMindAPI.Models;

namespace SecureMindAPI.Repository
{
    public class ReportRepository : IReportRepository
    {

        private ApplicationDbContext _context;

        public ReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async  Task<bool> AddReport(ReportDTO report)
        {
            if (report == null)
            {
                return false;
            }

            var incident = new Reports
            {
                Description = report.Description,
                ReportTime = report.ReportTime,
                Location = report.Location != null ? new Location
                {
                    Latitude = report.Location.Latitude,
                    Longitude = report.Location.Longitude
                } : null
            };
            _context.AnonymousReports.Add(incident);
            await _context.SaveChangesAsync();
            return true;
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
