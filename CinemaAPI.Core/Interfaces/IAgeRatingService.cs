using CinemaAPI.Core.Entities;
using CinemaAPI.Core.QueryFilters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CinemaAPI.Core.Interfaces
{
    public interface IAgeRatingService
    {
        IEnumerable<AgeRating> GetAgeRatings(AgeRatingQueryFilter filters);
        Task<AgeRating> GetAgeRating(int id);
        Task AddAgeRating(AgeRating ageRating);
        Task<bool> UpdateAgeRating(AgeRating ageRating);
        Task<bool> RemoveAgeRating(int id);
    }
}