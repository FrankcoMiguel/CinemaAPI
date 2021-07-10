using System;
using System.Collections.Generic;

namespace CinemaAPI.Core.Entities
{
    public partial class PersonOccupation : BaseEntity
    {
        public int? PersonId { get; set; }
        public int? OccupationId { get; set; }

        public virtual Occupation Occupation { get; set; }
        public virtual Person Person { get; set; }
    }
}
