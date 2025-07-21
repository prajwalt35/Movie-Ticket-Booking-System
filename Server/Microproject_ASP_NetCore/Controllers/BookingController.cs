using Microproject_ASP_NetCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Microproject_ASP_NetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BookingController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> BookTicket([FromBody] BookingRequestDto request)
        {
            //Even if you authorize, always validate the extracted user ID
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized(new { message = "User not found in token." });

            if (!int.TryParse(userIdClaim.Value, out int userId))
                return Unauthorized(new { message = "Invalid user ID." });

            var booking = new Booking
            {
                MovieId = request.MovieId,
                TheaterId = request.TheaterId,
                SeatNumber = request.SeatNumber,
                ShowTime = request.ShowTime,
                UserId = userId,
                BookingDate = DateTime.UtcNow
            };

            _context.Booking.Add(booking);

            // Mark seat as booked too, if needed
            var seat = await _context.Seats.FirstOrDefaultAsync(s => s.SeatNumber == request.SeatNumber);
            if (seat != null && !seat.IsBooked)
            {
                seat.IsBooked = true;
            }

            await _context.SaveChangesAsync();

            return Ok(new { message = "Booking successful", bookingId = booking.BookingId });
        }


        [HttpGet("mybookings")]
        [Authorize]
        public IActionResult GetMyBookings()
        {
            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdStr, out int userId))
                return Unauthorized(new { message = "Invalid user ID in token." });

            var bookings = _context.Booking
                .Include(b => b.Movie)
                .Include(b => b.Theater)
                .Where(b => b.UserId == userId)
                .Select(b => new
                {
                    b.BookingId,
                    b.SeatNumber,
                    b.ShowTime,
                    b.BookingDate,
                    Movie = b.Movie.Title,
                    Theater = b.Theater.TheaterName
                })
                .ToList();

            return Ok(bookings);
        }

    }
}
