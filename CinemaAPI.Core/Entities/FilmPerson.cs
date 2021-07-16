using System;
using System.Collections.Generic;

namespace CinemaAPI.Core.Entities
{
    public partial class FilmPerson : BaseEntity
    {
        public int? FilmId { get; set; }
        public int? PersonId { get; set; }

        public virtual Film Film { get; set; }
        public virtual Person Person { get; set; }
    }
}
