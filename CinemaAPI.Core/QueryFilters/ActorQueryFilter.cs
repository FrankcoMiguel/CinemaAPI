using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaAPI.Core.QueryFilters
{
    public class ActorQueryFilter
    {
        public int? Age { get; set; }
        public decimal? MinRating { get; set; }
        public decimal? MaxRating { get; set; }
    }
}
