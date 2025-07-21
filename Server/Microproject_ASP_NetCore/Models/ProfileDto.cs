namespace Microproject_ASP_NetCore.Models
{
    public class ProfileDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public IFormFile? ProfilePhoto { get; set; }
    }
}
