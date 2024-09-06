using SecureMindAPI.Models;

namespace SecureMindAPI.DTOs
{
    public class IncidentsDto
    {
        public int IncidentId { get; set; }
        public string Description { get; set; }
        public DateTime ReportTime { get; set; }
        public LocationDto Location { get; set; }
    }
}
