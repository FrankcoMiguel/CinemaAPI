using System;
using System.Collections.Generic;

namespace CinemaAPI.Core.Entities
{
    public partial class Genre : BaseEntity
    {
        public Genre()
        {
            FilmGenre = new HashSet<FilmGenre>();
        }

        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<FilmGenre> FilmGenre { get; set; }
    }
}
