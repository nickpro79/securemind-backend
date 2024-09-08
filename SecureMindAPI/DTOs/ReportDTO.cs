namespace SecureMindAPI.DTOs
{
    public class ReportDTO
    {
        public string Description { get; set; }
        public DateTime ReportTime { get; set; }
        public string Type { get; set; }
        public LocationDto Location { get; set; }
    }
}
