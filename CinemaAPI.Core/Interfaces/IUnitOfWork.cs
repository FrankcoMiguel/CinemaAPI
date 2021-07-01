using CinemaAPI.Core.Entities;
using System;
using System.Threading.Tasks;

namespace CinemaAPI.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IActorRepository ActorRepository { get; }
        IRepository<Crew> CrewRepository { get; }
        IRepository<CrewMember> CrewMemberRepository { get; }
        IRepository<CrewRole> CrewRoleRepository { get; }
        IRepository<Director> DirectorRepository { get; }
        IRepository<Film> FilmRepository { get; }

        void SaveChanges();
        Task SaveChangesAsync();

    }
}
