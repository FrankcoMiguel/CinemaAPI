using CinemaAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAPI.Core.Interfaces
{
    public interface IActorRepository
    {
        Task<IEnumerable<Actor>> GetActors();
        Task<Actor> GetActor(int id);
        Task AddActor(Actor actor);
        Task<bool> UpdateActor(Actor actor);
        Task<bool> RemoveActor(int id);

    }
}
