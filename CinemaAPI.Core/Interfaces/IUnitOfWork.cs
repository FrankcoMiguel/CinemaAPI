using System;
using System.Threading.Tasks;

namespace CinemaAPI.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IFilmRepository FilmRepository { get; }
        IGenreRepository GenreRepository { get; }
        IOccupationRepository OccupationRepository { get; }
        IPersonRepository PersonRepository { get;  }
        IRatingRepository RatingRepository { get; }


        void SaveChanges();
        Task SaveChangesAsync();

    }
}