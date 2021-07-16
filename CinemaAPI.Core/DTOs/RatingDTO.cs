using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaAPI.Core.DTOs
{
    public class RatingDTO
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
