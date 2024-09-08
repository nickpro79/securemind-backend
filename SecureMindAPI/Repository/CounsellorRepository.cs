using Microsoft.EntityFrameworkCore;
using SecureMindAPI.Contract;
using SecureMindAPI.Data;
using SecureMindAPI.DTOs;

namespace SecureMindAPI.Repository
{
    public class CounsellorRepository : ICounsellorsRepository
    {
        private ApplicationDbContext _context;
        public CounsellorRepository(ApplicationDbContext context)
        {
           _context = context;
        }
        public Task<IEnumerable<CounsellerDTO>> FilterBySpecialization(string specialization)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CounsellerDTO>> GetAll()
        {
            var Counsellors = await _context.MentalHealthProfessionals.ToListAsync();
            var result = new List<CounsellerDTO>();
            Counsellors.ForEach(c =>
            {
                CounsellerDTO temp = new CounsellerDTO()
                {
                    Name = c.Name,
                    Specialization = c.Specialization,
                    ContactInfo = c.ContactInfo,
                };
                result.Add(temp);
            });

            return result;
        }
    }
}
