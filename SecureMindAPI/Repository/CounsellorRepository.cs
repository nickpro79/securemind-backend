using Microsoft.EntityFrameworkCore;
using SecureMindAPI.Contract;
using SecureMindAPI.Data;
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
            {
                var counsellors = await _context.MentalHealthProfessionals
                    .Where(c => c.Specialization.Contains(specialization))
                    .ToListAsync();

                return counsellors.Select(c => new CounsellerResponseDTO
                {
                    Name = c.Name,
                    Specialization = c.Specialization,
                    ContactInfo = c.ContactInfo
                });
            }
        }

        public async Task<IEnumerable<CounsellerResponseDTO>> GetAll()
        {
            var counsellors = await _context.MentalHealthProfessionals.ToListAsync();
            var result = counsellors.Select(c => new CounsellerResponseDTO
            {
                Name = c.Name,
                Specialization = c.Specialization,
                ContactInfo = c.ContactInfo
            });
            return result;
        }
    }
}
