namespace Microproject_ASP_NetCore.Models
{
    public class ShowtimeResponseDto
    {
        public int TheaterId { get; set; }
        public string TheaterName { get; set; }
        public List<string> ShowTimes { get; set; }
    }

}
