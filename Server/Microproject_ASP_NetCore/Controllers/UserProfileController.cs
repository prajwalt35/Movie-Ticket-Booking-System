using Microproject_ASP_NetCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

//[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserProfileController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _env;

    public UserProfileController(AppDbContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }

    // GET api/UserProfile
    [HttpGet]
    public IActionResult GetProfile()
    {
        var email = User.FindFirst(ClaimTypes.Email)?.Value;
        var user = _context.Users.FirstOrDefault(u => u.Email == email);

        if (user == null)
            return NotFound();

        return Ok(new
        {
            user.FullName,
            user.Gender,
            user.MobileNumber,
            user.Email,
            user.ProfilePhotoUrl
        });
    }

    // PUT api/UserProfile
    [HttpPut("UpdateProfile")]
    public async Task<IActionResult> UpdateProfile([FromForm] ProfileDto dto)
    {
        var email = User.FindFirst(ClaimTypes.Email)?.Value;
        var user = _context.Users.FirstOrDefault(u => u.Email == email);
        if (user == null) return NotFound();

        user.FullName = dto.FirstName + dto.LastName;
        user.Gender = dto.Gender;
        user.MobileNumber = dto.MobileNumber;

        if (dto.ProfilePhoto != null)
        {
            var uploadsFolder = Path.Combine(_env.WebRootPath, "ProfilePhotos");
            Directory.CreateDirectory(uploadsFolder);
            var fileName = Guid.NewGuid() + Path.GetExtension(dto.ProfilePhoto.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await dto.ProfilePhoto.CopyToAsync(stream);
            }

            user.ProfilePhotoUrl = $"ProfilePhotos/{fileName}";
        }

        await _context.SaveChangesAsync();
        return Ok("Profile updated successfully");
    }
}


