using CinemaAPI.Core.CustomEntities;
using CinemaAPI.Core.Entities;
using CinemaAPI.Core.QueryFilters;
using System.Threading.Tasks;

namespace CinemaAPI.Core.Interfaces
{
    public interface IFilmService
    {
        PagedList<Film> GetFilms(FilmQueryFilter filters);
        Task<Film> GetFilm(int id);
        Task AddFilm(Film film);
        Task<bool> UpdateFilm(Film film);
        Task<bool> RemoveFilm(int id);
    }
}
