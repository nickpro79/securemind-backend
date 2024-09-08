namespace SecureMindAPI.DTOs
{
    public class RegisterDTO
    {
        public int LocationId { get; set; }
        public string Description { get; set; }
        public DateTime ReportTime { get; set; }
        public string Type { get; set; }
    }
}
