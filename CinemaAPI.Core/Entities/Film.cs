using System;
using System.Collections.Generic;

namespace CinemaAPI.Core.Entities
{
    public partial class Film : BaseEntity
    {
        public Film()
        {
            Casting = new HashSet<Casting>();
            Genres = new HashSet<Genres>();
        }

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
        public virtual ICollection<Casting> Casting { get; set; }
        public virtual ICollection<Genres> Genres { get; set; }
    }
}
