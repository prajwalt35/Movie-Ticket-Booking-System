namespace Microproject_ASP_NetCore.Models
{
    public class Theater
    {
        public int TheaterId { get; set; }
        public string TheaterName { get; set; }
        public ICollection<ShowTime> ShowTimes { get; set; }
    }
}
