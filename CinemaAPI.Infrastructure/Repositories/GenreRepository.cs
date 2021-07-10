using CinemaAPI.Core.Entities;
using CinemaAPI.Core.Interfaces;
using CinemaAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CinemaAPI.Infrastructure.Repositories
{
    public class GenreRepository : BaseRepository<Genre>, IGenreRepository
    {
        public GenreRepository(CinemaAPIContext context) : base(context) { }

        //Custom method
        public async Task<IEnumerable<Genre>> GetGenresByMovie(int filmId)
        {
            return await _entities.Where(x => x.Id == filmId).ToListAsync();
        }



    }
}