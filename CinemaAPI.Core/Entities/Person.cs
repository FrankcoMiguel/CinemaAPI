using System;
using System.Collections.Generic;

namespace CinemaAPI.Core.Entities
{
    public partial class Person : BaseEntity
    {
        public Person()
        {
            Casting = new HashSet<Casting>();
            Occupations = new HashSet<Occupations>();
        }

        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Description { get; set; }
        public int? Age { get; set; }
        public string IsActive { get; set; }
        public string PictureReference { get; set; }

        public virtual ICollection<Casting> Casting { get; set; }
        public virtual ICollection<Occupations> Occupations { get; set; }
    }
}
