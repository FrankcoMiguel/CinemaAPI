using CinemaAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAPI.Core.Interfaces
{
    public interface IAgeRatingRepository : IRepository<AgeRating>
    {
        Task<IEnumerable<AgeRating>> GetAgeRatingsByFilms(int filmId);

    }
}
