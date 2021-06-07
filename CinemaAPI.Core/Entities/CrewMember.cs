using System;
using System.Collections.Generic;

namespace CinemaAPI.Core.Entities
{
    public partial class CrewMember
    {
        public int CrewMemberId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Image { get; set; }
        public int CrewRoleId { get; set; }
        public int CrewId { get; set; }

        public virtual Crew Crew { get; set; }
        public virtual CrewRole CrewRole { get; set; }
    }
}
