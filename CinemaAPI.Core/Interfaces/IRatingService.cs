using CinemaAPI.Core.CustomEntities;
using CinemaAPI.Core.Entities;
using CinemaAPI.Core.QueryFilters;
using System.Threading.Tasks;

namespace CinemaAPI.Core.Interfaces
{
    public interface IRatingService
    {
        PagedList<Rating> GetRatings(GenreQueryFilter filters);
        Task<Rating> GetRating(int id);
        Task AddRating(Rating rating);
        Task<bool> UpdateRating(Rating rating);
        Task<bool> RemoveRating(int id);
    }
}
