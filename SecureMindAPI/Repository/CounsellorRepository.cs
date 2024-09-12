using Microsoft.EntityFrameworkCore;
using SecureMindAPI.Contract;
using SecureMindAPI.Data;
using SecureMindAPI.DTOs;
using SecureMindAPI.DTOs.CounsellorRequestDTO;

namespace SecureMindAPI.Repository
{
    public class CounsellorRepository : ICounsellorsRepository
    {
        private ApplicationDbContext _context;
        public CounsellorRepository(ApplicationDbContext context)
        {
           _context = context;
        }
        public async Task<IEnumerable<CounsellerResponseDTO>> FilterBySpecialization(string specialization)
        {
            return await _context.MentalHealthProfessionals
            .Include(i => i.Location)
            .Where(i=> i.Specialization == specialization)
            .Select(i => new CounsellerResponseDTO
               {
                Name = i.Name,
                Specialization=i.Specialization,
                ContactInfo = i.ContactInfo,
                Location = new LocationDto
                {
                Latitude = i.Location.Latitude,
                Longitude = i.Location.Longitude
                 }
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<CounsellerResponseDTO>> GetAll()
        {
            var counsellors = await _context.MentalHealthProfessionals.ToListAsync();
            var result = counsellors.Select(c => new CounsellerResponseDTO
            {
                Name = c.Name,
                Specialization = c.Specialization,
                ContactInfo = c.ContactInfo,
                Location=new LocationDto { 
                    Latitude = c.Location.Latitude, 
                    Longitude=c.Location.Longitude
                }
           
            });
            return result;
        }
    }
}
