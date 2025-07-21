namespace Microproject_ASP_NetCore.Models
{
    public class Movie
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string BannerUrl { get; set; }
        public string PosterUrl { get; set; }
        public string Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Duration { get; set; }

        public ICollection<ShowTime> ShowTimes { get; set; }
    }

}
