using System;
using System.Threading.Tasks;

namespace CinemaAPI.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IAgeRatingRepository AgeRatingRepository { get; }
        IGenreRepository GenreRepository { get; }


        void SaveChanges();
        Task SaveChangesAsync();

    }
}