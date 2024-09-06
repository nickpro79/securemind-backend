namespace SecureMindAPI.Models
{
    public class Incidents
    {
        public string IncidentId { get; set; }
        public string LocationId { get; set; }
        public string Description { get; set; }
        public DateTime ReportTime { get; set; }

        public Location Location { get; set; }
    }
}
