namespace Microproject_ASP_NetCore.Models
{
    public class BookingRequestDto
    {
        public int MovieId { get; set; }
        public int TheaterId { get; set; }
        public string SeatNumber { get; set; }
        public string ShowTime { get; set; }
    }
}
