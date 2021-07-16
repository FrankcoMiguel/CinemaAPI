using System;
using System.Collections.Generic;

namespace CinemaAPI.Core.Entities
{
    public partial class Occupation : BaseEntity
    {
        public Occupation()
        {
            PersonFilmOccupation = new HashSet<PersonFilmOccupation>();
            PersonOccupation = new HashSet<PersonOccupation>();
        }

        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<PersonFilmOccupation> PersonFilmOccupation { get; set; }
        public virtual ICollection<PersonOccupation> PersonOccupation { get; set; }
    }
}
