using System;
using System.Threading.Tasks;

namespace CinemaAPI.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICastingRepository CastingRepository { get; }
        IFilmRepository FilmRepository { get; }
        IGenreRepository GenreRepository { get; }
        IOccupationRepository OccupationRepository { get; }
        IPersonRepository PersonRepository { get;  }
        IRatingRepository RatingRepository { get; }
        IUserRepository UserRepository { get; }



        void SaveChanges();
        Task SaveChangesAsync();

    }
}