namespace Microproject_ASP_NetCore.Models
{
    public class Seat
    {
        public int Id { get; set; }
        public string SeatNumber { get; set; } = string.Empty;
        public bool IsBooked { get; set; }
    }
}
