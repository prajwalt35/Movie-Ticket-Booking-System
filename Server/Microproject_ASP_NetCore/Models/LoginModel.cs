using System.ComponentModel.DataAnnotations;

namespace Microproject_ASP_NetCore.Models
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
