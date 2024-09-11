namespace SecureMindAPI.DTOs.CounsellorRequestDTO
{
    public class CounsellerResponseDTO
    {
        public string Name { get; set; }
        public string Specialization { get; set; }
        public string ContactInfo { get; set; }

        public LocationDto Location { get; set; }
    }
}
