﻿using CinemaAPI.Core.Entities;
using CinemaAPI.Core.QueryFilters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CinemaAPI.Core.Interfaces
{
    public interface IActorService
    {
        IEnumerable<Actor> GetActors(ActorQueryFilter filters);
        Task<Actor> GetActor(int id);
        Task AddActor(Actor actor);
        Task<bool> UpdateActor(Actor actor);
        Task<bool> RemoveActor(int id);
    }
}