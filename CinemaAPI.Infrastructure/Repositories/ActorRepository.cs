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
    public class ActorRepository : BaseRepository<Actor>, IActorRepository
    {
        public ActorRepository(CinemaAPIContext context) : base(context) { }

        public async Task<IEnumerable<Actor>> GetActorsByFilm(int filmId)
        {
            return await _entities.Where(x => x.Id == filmId).ToListAsync();
        }
    }
}
