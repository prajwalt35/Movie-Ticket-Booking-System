using Microproject_ASP_NetCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Microproject_ASP_NetCore.Controllers
{
    [Route("api/seats")]
    [ApiController]
    public class SeatController : ControllerBase
    {
        private readonly AppDbContext _context;
        public SeatController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Seat>>> GetSeats()
        {
            return await _context.Seats.ToListAsync();
        }

        [HttpPost("book/{id}")]
        public async Task<IActionResult> BookSeat(int id)
        {
            var seat = await _context.Seats.FindAsync(id);
            if (seat == null)
            {
                return NotFound("Seat not found");
            }
            if (seat.IsBooked)
            {
                return BadRequest("Seat is already booked");
            }

            seat.IsBooked = true;
            await _context.SaveChangesAsync();

            return Ok(new { message = "Seat booked successfully", seat });
        }

        [HttpPost("cancel/{id}")]
        public async Task<IActionResult> CancelSeat(int id)
        {
            var seat = await _context.Seats.FindAsync(id);
            if (seat == null)
            {
                return NotFound("Seat not found");
            }
            if (!seat.IsBooked)
            {
                return BadRequest("Seat is not booked");
            }

            seat.IsBooked = false;
            await _context.SaveChangesAsync();

            return Ok(new { message = "Seat cancelled successfully", seat });
        }

    }
}
