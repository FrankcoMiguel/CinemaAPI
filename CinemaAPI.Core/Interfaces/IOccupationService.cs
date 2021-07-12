using CinemaAPI.Core.CustomEntities;
using CinemaAPI.Core.Entities;
using CinemaAPI.Core.QueryFilters;
using System.Threading.Tasks;

namespace CinemaAPI.Core.Interfaces
{
    public interface IOccupationService
    {
        PagedList<Occupation> GetOccupations(FilmQueryFilter filters);
        Task<Occupation> GetOccupation(int id);
        Task AddOccupation(Occupation occupation);
        Task<bool> UpdateOccupation(Occupation occupation);
        Task<bool> RemoveOccupation(int id);
    }
}
