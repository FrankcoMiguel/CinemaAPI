using System;
using System.Collections.Generic;

namespace CinemaAPI.Core.Entities
{
    public partial class Genre : BaseEntity
    {
        public Genre()
        {
            Genres = new HashSet<Genres>();
        }

        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Genres> Genres { get; set; }
    }
}
