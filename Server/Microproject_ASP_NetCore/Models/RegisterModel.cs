using System.ComponentModel.DataAnnotations;

namespace Microproject_ASP_NetCore.Models
{
    public class RegisterModel
    {
        [Required]
        public string? FullName { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }

        [Required]
        public string? Role { get; set; }
    }
}
