using CinemaAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAPI.Core.Interfaces
{
    public interface IFilmRepository
    {
        Task<IEnumerable<Film>> GetFilms();
        Task<Film> GetFilm(int id);
    }
}
