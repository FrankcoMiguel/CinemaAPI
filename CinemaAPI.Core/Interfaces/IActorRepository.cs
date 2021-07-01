using CinemaAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAPI.Core.Interfaces
{
    public interface IActorRepository : IRepository<Actor>
    {
        Task<IEnumerable<Actor>> GetActorsByFilm(int filmId);
    }
}
