using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureMindAPI.Contract;
using SecureMindAPI.Data;
using SecureMindAPI.DTOs;

namespace SecureMindAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrimeIncidentsController : ControllerBase
    {
        private readonly ICrimeIncident _crimeIncident;
        public CrimeIncidentsController(ICrimeIncident crimeIncident)
        {
            _crimeIncident = crimeIncident;
        }
        public async Task<ActionResult<IEnumerable<IncidentsDto>>> GetAllIncidents()
        {
            // Await the result from the service
            var incidents = await _crimeIncident.GetAllIncidentsAsync();

            if (incidents == null || !incidents.Any())
            {
                return NotFound(new { message = "No incidents found" });
            }

            return Ok(incidents);
        }
    }

}

