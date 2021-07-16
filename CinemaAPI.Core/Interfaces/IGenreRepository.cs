using CinemaAPI.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CinemaAPI.Core.Interfaces
{
    public interface IGenreRepository : IRepository<Genre>
    {
        Task<IEnumerable<Genre>> GetGenresByMovie(int filmId);
    }
}
