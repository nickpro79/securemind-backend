namespace SecureMindAPI.Models
{
    public class Location
    {
        public int LocationId { get; set; }
        public string Name { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }

        public ICollection<Incidents> CrimeIncidents { get; set; }
        public ICollection<Reports> AnonymousReports { get; set; }
        public ICollection<Counsellors> MentalHealthProfessionals { get; set; }
    }
}
