namespace Microproject_ASP_NetCore.Models
{
    public class Booking
    {
        public Guid BookingId { get; set; } = Guid.NewGuid();

        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public int TheaterId { get; set; }
        public Theater Theater { get; set; }

        public string SeatNumber { get; set; }
        public string ShowTime { get; set; }

        public DateTime BookingDate { get; set; } = DateTime.UtcNow;
        public int UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
