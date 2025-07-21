using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microproject_ASP_NetCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Microproject_ASP_NetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public UserAuthController(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (string.IsNullOrWhiteSpace(model.FullName) ||
                string.IsNullOrWhiteSpace(model.Email) ||
                string.IsNullOrWhiteSpace(model.Password) ||
                string.IsNullOrWhiteSpace(model.Role))
            {
                return BadRequest("Please provide all required fields.");
            }

            if (await _context.Users.AnyAsync(u => u.Email == model.Email))
                return Conflict("Email already exists.");

            var user = new ApplicationUser
            {
                FullName = model.FullName,
                Email = model.Email,
                Role = model.Role,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password),
                Gender = string.Empty,
                MobileNumber = string.Empty,
                ProfilePhotoUrl = string.Empty
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("Registered successfully");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == model.Email);

            if (user == null || string.IsNullOrEmpty(user.PasswordHash) ||
                !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
            {
                return Unauthorized("Invalid credentials");
            }

            var token = GenerateJwtToken(user);
            return Ok(new { token });
        }
        private string GenerateJwtToken(ApplicationUser user)
        {
            //var claims = new[]
            //{
            //    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            //    new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
            //    new Claim("role", user.Role ?? string.Empty),
            //    new Claim(ClaimTypes.Role, user.Role ?? string.Empty),
            //    new Claim("FullName", user.FullName ?? string.Empty)
            //};
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),         // used in UserProfileController
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),           // used in BookingController
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.Role, "User"),
                new Claim("FullName", user.FullName ?? string.Empty)
            };


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["jwt:Issuer"],
                audience: _config["jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}