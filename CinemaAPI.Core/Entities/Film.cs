using System;

namespace CinemaAPI.Core.Entities
{
    public partial class Film
    {
        public int FilmId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Duration { get; set; }
        public decimal Rating { get; set; }
        public string Image { get; set; }
        public int DirectorId { get; set; }

        public virtual Director Director { get; set; }
    }
}
