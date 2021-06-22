using System;
using System.Collections.Generic;

namespace CinemaAPI.Core.Entities
{
    public partial class CrewRole : BaseEntity
    {
        public CrewRole()
        {
            CrewMember = new HashSet<CrewMember>();
        }

        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<CrewMember> CrewMember { get; set; }
    }
}
