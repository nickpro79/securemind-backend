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
        public async Task<bool> AddReport(ReportDTO report)
        {
                if (report == null)
                {
                    throw new ArgumentNullException(nameof(report));
                }

                var incident = new Reports
                {
                    Description = report.Description,
                    ReportTime = report.ReportTime,
                    Type = report.Type,
                };

                if (report.Location != null)
                {
                    var existingLocation = await _context.Locations
                        .FirstOrDefaultAsync(l => l.Latitude == report.Location.Latitude && l.Longitude == report.Location.Longitude);

                    if (existingLocation != null)
                    {
                        incident.Location = existingLocation;
                    }
                    else
                    {
                        var newLocation = new Location
                        {   
                            Name="",
                            Latitude = report.Location.Latitude,
                            Longitude = report.Location.Longitude
                        };

                        _context.Locations.Add(newLocation);
                        await _context.SaveChangesAsync();

                        incident.Location = newLocation;
                    }
                }

                try
                {
                    _context.AnonymousReports.Add(incident);

                    await _context.SaveChangesAsync();

                    return true;
                }
                catch (Exception ex)
                { 
                    return false;
                }
            }

        public async Task<IEnumerable<ReportDTO>> GetAllReports()
        {
            return await _context.CrimeIncidents
            .Include(i => i.Location) 
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
