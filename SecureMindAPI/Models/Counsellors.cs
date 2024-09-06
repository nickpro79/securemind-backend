namespace SecureMindAPI.Models
{
    public class Counsellors
    {
        public int CounsellorId { get; set; }
        public int LocationId { get; set; }
        public string Name { get; set; }
        public string Specialization { get; set; }
        public string ContactInfo { get; set; }

        public Location Location { get; set; }
    }
}
