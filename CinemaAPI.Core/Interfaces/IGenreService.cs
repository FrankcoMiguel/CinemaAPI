using CinemaAPI.Core.CustomEntities;
using CinemaAPI.Core.Entities;
using CinemaAPI.Core.QueryFilters;
using System.Threading.Tasks;

namespace CinemaAPI.Core.Interfaces
{
    public interface IGenreService
    {
        PagedList<Genre> GetGenres(GenreQueryFilter filters);
        Task<Genre> GetGenre(int id);
        Task AddGenre(Genre genre);
        Task<bool> UpdateGenre(Genre genre);
        Task<bool> RemoveGenre(int id);
    }
}
