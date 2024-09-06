namespace SecureMindAPI.Models
{
    public class Reports
    {
        public string ReportId { get; set; }
        public string LocationId { get; set; }
        public string Description { get; set; }
        public DateTime ReportTime { get; set; }
        public string Type { get; set; }

        public Location Location { get; set; }
    }
}
