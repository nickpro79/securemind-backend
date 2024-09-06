namespace SecureMindAPI.Models
{
    public class Reports
    {
        public int ReportId { get; set; }
        public int LocationId { get; set; }
        public string Description { get; set; }
        public DateTime ReportTime { get; set; }
        public string Type { get; set; }

        public Location Location { get; set; }
    }
}
