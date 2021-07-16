﻿using CinemaAPI.Core.Entities;
using CinemaAPI.Core.Interfaces;
using CinemaAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CinemaAPI.Infrastructure.Repositories
{
    public class AgeRatingRepository : BaseRepository<AgeRating>, IAgeRatingRepository
    {
        public AgeRatingRepository(CinemaAPIContext context) : base(context) { }

        //Custom method
        public async Task<IEnumerable<AgeRating>> GetAgeRatingsByFilms(int filmId)
        {
            return await _entities.Where(x => x.Id == filmId).ToListAsync();
        }

        

    }
}
