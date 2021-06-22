using CinemaAPI.Core.Entities;
using CinemaAPI.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAPI.Core.Services
{
    public class ActorService : IActorService
    {
        private readonly IActorRepository _actorRepository;

        public ActorService(IActorRepository actorRepository)
        {
            _actorRepository = actorRepository;
        }

        public async Task<IEnumerable<Actor>> GetActors()
        {
            return await _actorRepository.GetActors();
        }


        public async Task<Actor> GetActor(int id)
        {
            return await _actorRepository.GetActor(id);
        }

        public async Task AddActor(Actor actor)
        {
            await _actorRepository.AddActor(actor);
        }

        public async Task<bool> UpdateActor(Actor actor)
        {
            return await _actorRepository.UpdateActor(actor);
        }

        public async Task<bool> RemoveActor(int id)
        {
            return await _actorRepository.RemoveActor(id);
        }

    }
}
