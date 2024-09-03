namespace AuthenticationAPI.Models
{
    public class User
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime RegistrationDate { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }
}
