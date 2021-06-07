﻿using System;
using System.Collections.Generic;

namespace CinemaAPI.Core.Entities
{
    public partial class Director
    {
        public Director()
        {
            Film = new HashSet<Film>();
        }

        public int DirectorId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public decimal Rating { get; set; }
        public string Image { get; set; }

        public virtual ICollection<Film> Film { get; set; }
    }
}
