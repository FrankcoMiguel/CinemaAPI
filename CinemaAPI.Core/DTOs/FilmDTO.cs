using CinemaAPI.Core.Entities;

namespace CinemaAPI.Core.DTOs
{
    public class FilmDTO
    {
        public string Title { get; set; }
        public string Synopsis { get; set; }
        public int ReleaseYear { get; set; }
        public int RunningTime { get; set; }
        public string Language { get; set; }
        public int? RatingId { get; set; }
        public decimal? Score { get; set; }
        public decimal? Budget { get; set; }
        public decimal? BoxOffice { get; set; }
        public string PictureReference { get; set; }
        public virtual Rating Rating { get; set; }
    }
}
