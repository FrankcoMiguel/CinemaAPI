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
    public class DirectorRepository : IDirectorRepository
    {

        private readonly CinemaAPIContext _context;

        public DirectorRepository(CinemaAPIContext context)
        {
            _context = context;
        }

        public async Task<Director> GetDirector(int id)
        {
            var director = await _context.Director.SingleAsync(x => x.DirectorId == id);
            return director;
        }

        public async Task<IEnumerable<Director>> GetDirectors()
        {
            var directors = await _context.Director.ToListAsync();
            return directors;
        }
    }
}
