using CinemaAPI.Core.Entities;
using CinemaAPI.Core.Interfaces;
using CinemaAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaAPI.Infrastructure.Repositories
{
    public class FilmRepository : BaseRepository<Film>, IFilmRepository
    {
        public FilmRepository(CinemaAPIContext context) : base(context) { }

        public async Task<IEnumerable<Film>> GetGenresByMovie(int genreId)
        {
            return await _entities.Where(x => x.Id == genreId).ToListAsync();
        }
    }
}
