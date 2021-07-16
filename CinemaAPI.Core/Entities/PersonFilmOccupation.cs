using System;
using System.Collections.Generic;

namespace CinemaAPI.Core.Entities
{
    public partial class PersonFilmOccupation : BaseEntity
    {
        public int? PersonId { get; set; }
        public int? FilmId { get; set; }
        public int? OccupationId { get; set; }

        public virtual Film Film { get; set; }
        public virtual Occupation Occupation { get; set; }
        public virtual Person Person { get; set; }
    }
}
