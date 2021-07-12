using CinemaAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAPI.Core.Interfaces
{
    public interface IFilmRepository : IRepository<Film>
    {
        Task<IEnumerable<Film>> GetGenresByMovie(int genreId);
    }
}
