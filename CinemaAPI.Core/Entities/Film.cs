using System;
using System.Collections.Generic;

namespace CinemaAPI.Core.Entities
{
    public partial class Film : BaseEntity
    {
        public Film()
        {
            FilmGenre = new HashSet<FilmGenre>();
            FilmPerson = new HashSet<FilmPerson>();
            PersonFilmOccupation = new HashSet<PersonFilmOccupation>();
        }

        public string Title { get; set; }
        public string Synopsis { get; set; }
        public int ReleaseYear { get; set; }
        public int RunningTime { get; set; }
        public string Language { get; set; }
        public int? AgeRatingId { get; set; }
        public decimal Rating { get; set; }
        public int Budget { get; set; }
        public int BoxOffice { get; set; }

        public virtual AgeRating AgeRating { get; set; }
        public virtual ICollection<FilmGenre> FilmGenre { get; set; }
        public virtual ICollection<FilmPerson> FilmPerson { get; set; }
        public virtual ICollection<PersonFilmOccupation> PersonFilmOccupation { get; set; }
    }
}
