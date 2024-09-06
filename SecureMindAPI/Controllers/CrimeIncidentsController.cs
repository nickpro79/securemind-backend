using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureMindAPI.Data;

namespace SecureMindAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrimeIncidentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CrimeIncidentsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllIncidents()
        {
            var incidents = await _context.CrimeIncidents
                .Include(i => i.Location) // Include location if needed
                .ToListAsync();

            if (incidents == null || !incidents.Any())
            {
                return NotFound(new { message = "No incidents found" });
            }

            return Ok(incidents);
        }



    }
}
