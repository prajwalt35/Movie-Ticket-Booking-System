namespace Microproject_ASP_NetCore.Models
{
    public class ShowTime
    {
        public int ShowTimeId { get; set; }
        public string Time { get; set; }

        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        public int TheaterId { get; set; }
        public Theater Theater { get; set; }
    }

}
