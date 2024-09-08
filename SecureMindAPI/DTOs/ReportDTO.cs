namespace SecureMindAPI.DTOs
{
    public class ReportDTO
    {
        public int LocationId { get; set; }
        public string Description { get; set; }
        public DateTime ReportTime { get; set; }
        public string Type { get; set; }
    }
}
