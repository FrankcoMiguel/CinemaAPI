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

        public IEnumerable<Actor> GetActors()
        {
            return _unitOfWork.ActorRepository.GetAll();
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
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateActor(Actor actor)
        {
            //We can add business logic here :)
            _unitOfWork.ActorRepository.Update(actor);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveActor(int id)
        {
            //We can add business logic here :)
            await _unitOfWork.ActorRepository.Delete(id);
            _unitOfWork.SaveChanges();
            return true;
        }

    }
}
