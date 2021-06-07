using System;
using System.Collections.Generic;

namespace CinemaAPI.Core.Entities
{
    public partial class Actor
    {
        public int ActorId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public decimal Rating { get; set; }
        public string Image { get; set; }
    }
}
