using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Microproject_ASP_NetCore.Models
{
    [Table("Users")]
    public class ApplicationUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [Required]
        public string Role { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string MobileNumber { get; set; }
        public string ProfilePhotoUrl { get; set; }
    }
}
