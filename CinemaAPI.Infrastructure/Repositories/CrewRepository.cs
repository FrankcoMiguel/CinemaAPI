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
    public class CrewRepository : ICrewRepository
    {

        private readonly CinemaAPIContext _context;

        public CrewRepository(CinemaAPIContext context)
        {
            _context = context;
        }

        public async Task<Crew> GetCrew(int id)
        {
            var crew = await _context.Crew.SingleAsync(x => x.CrewId == id);
            return crew;
        }

        public async Task<IEnumerable<Crew>> GetCrews()
        {
            var crews = await _context.Crew.ToListAsync();
            return crews;
        }
    }
}
