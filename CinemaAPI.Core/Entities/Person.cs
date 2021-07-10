using System;
using System.Collections.Generic;

namespace CinemaAPI.Core.Entities
{
    public partial class Person : BaseEntity
    {
        public Person()
        {
            FilmPerson = new HashSet<FilmPerson>();
            PersonFilmOccupation = new HashSet<PersonFilmOccupation>();
            PersonOccupation = new HashSet<PersonOccupation>();
        }

        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Description { get; set; }
        public int? Age { get; set; }
        public string IsActive { get; set; }
        public string PictureReference { get; set; }

        public virtual ICollection<FilmPerson> FilmPerson { get; set; }
        public virtual ICollection<PersonFilmOccupation> PersonFilmOccupation { get; set; }
        public virtual ICollection<PersonOccupation> PersonOccupation { get; set; }
    }
}
