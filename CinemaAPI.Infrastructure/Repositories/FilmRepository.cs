using CinemaAPI.Core.Entities;
using CinemaAPI.Core.Interfaces;
using CinemaAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAPI.Infrastructure.Repositories
{
    public class FilmRepository : IFilmRepository
    {

        private readonly CinemaAPIContext _context;

        public FilmRepository(CinemaAPIContext context)
        {
            _context = context;
        }

        public async Task<Film> GetFilm(int id)
        {
            var film = await _context.Film.SingleAsync(x => x.FilmId == id);
            return film;
        }

        public async Task<IEnumerable<Film>> GetFilms()
        {
            var films = await _context.Film.ToListAsync();
            return films;
        }
    }
}
