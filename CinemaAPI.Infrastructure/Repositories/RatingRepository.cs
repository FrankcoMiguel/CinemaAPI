using CinemaAPI.Core.Entities;
using CinemaAPI.Core.Interfaces;
using CinemaAPI.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaAPI.Infrastructure.Repositories
{
    public class RatingRepository : BaseRepository<Rating>, IRatingRepository
    {
        public RatingRepository(CinemaAPIContext context) : base(context) { }
    }
}
