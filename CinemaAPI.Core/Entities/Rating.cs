using System;
using System.Collections.Generic;

namespace CinemaAPI.Core.Entities
{
    public partial class Rating : BaseEntity
    {
        public Rating()
        {
            Film = new HashSet<Film>();
        }

        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Film> Film { get; set; }
    }
}
