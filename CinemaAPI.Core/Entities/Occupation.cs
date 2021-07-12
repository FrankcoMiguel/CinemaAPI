using System;
using System.Collections.Generic;

namespace CinemaAPI.Core.Entities
{
    public partial class Occupation : BaseEntity
    {
        public Occupation()
        {
            Casting = new HashSet<Casting>();
            Occupations = new HashSet<Occupations>();
        }

        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Casting> Casting { get; set; }
        public virtual ICollection<Occupations> Occupations { get; set; }
    }
}
