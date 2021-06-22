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
        private readonly IUnitOfWork _unitOfWork;

        public ActorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Actor>> GetActors()
        {
            return await _unitOfWork.ActorRepository.GetAll();
        }


        public async Task<Actor> GetActor(int id)
        {
            //We can add business logic here :)
            return await _unitOfWork.ActorRepository.GetById(id);
        }

        public async Task AddActor(Actor actor)
        {
            //We can add business logic here :)
            await _unitOfWork.ActorRepository.Add(actor);
        }

        public async Task<bool> UpdateActor(Actor actor)
        {
            //We can add business logic here :)
            await _unitOfWork.ActorRepository.Update(actor);
            return true;
        }

        public async Task<bool> RemoveActor(int id)
        {
            //We can add business logic here :)
            await _unitOfWork.ActorRepository.Delete(id);
            return true;
        }

    }
}
