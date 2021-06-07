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
    public class ActorRepository : IActorRepository
    {

        private readonly CinemaAPIContext _context;

        public ActorRepository(CinemaAPIContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Actor>> GetActors()
        {
            var actors = await _context.Actor.ToListAsync();
            return actors;
        }


        public async Task<Actor> GetActor(int id)
        {
            var actor = await _context.Actor.SingleAsync(x => x.ActorId == id);
            return actor;
        }

        public async Task AddActor(Actor actor)
        {
            _context.Actor.Add(actor);
            await _context.SaveChangesAsync();
        }


    }
}
