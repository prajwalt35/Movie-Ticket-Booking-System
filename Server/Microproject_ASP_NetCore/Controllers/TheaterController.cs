using Microproject_ASP_NetCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Microproject_ASP_NetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TheaterController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TheaterController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /api/theater/movie/1
        [HttpGet("movies/{movieId}")]
        public async Task<IActionResult> GetShowtimesByMovie(int movieId)
        {
            var theaters = await _context.Theaters
                .Include(t => t.ShowTimes.Where(s => s.MovieId == movieId))
                .ToListAsync();

            var result = theaters.Select(t => new ShowtimeResponseDto
            {
                TheaterId = t.TheaterId,
                TheaterName = t.TheaterName,
                ShowTimes = t.ShowTimes.Select(s => s.Time).ToList()
            });

            return Ok(result);
        }

        // GET: api/theater/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTheater(int id)
        {
            var theater = await _context.Theaters.FindAsync(id);
            if (theater == null) return NotFound();
            return Ok(theater);
        }


        // POST: /api/theater (Admin)
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddTheater(TheaterDto dto)
        {
            var theater = new Theater { TheaterName = dto.TheaterName };
            _context.Theaters.Add(theater);
            await _context.SaveChangesAsync();
            return Ok(theater);
        }

        // PUT: /api/theater/1
        [HttpPut("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateTheater(int id, TheaterDto dto)
        {
            var theater = await _context.Theaters.FindAsync(id);
            if (theater == null) return NotFound();

            theater.TheaterName = dto.TheaterName;
            await _context.SaveChangesAsync();
            return Ok(theater);
        }

        // DELETE: /api/theater/1
        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteTheater(int id)
        {
            var theater = await _context.Theaters.FindAsync(id);
            if (theater == null) return NotFound();

            _context.Theaters.Remove(theater);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
