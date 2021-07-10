using CinemaAPI.Core.Interfaces;
using CinemaAPI.Infrastructure.Data;
using System.Threading.Tasks;

namespace CinemaAPI.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CinemaAPIContext _context;

        private readonly IAgeRatingRepository _ageRatingRepository;
        private readonly IGenreRepository _genreRepository;

        public UnitOfWork(CinemaAPIContext context)
        {
            _context = context;
        }

        public IAgeRatingRepository AgeRatingRepository => _ageRatingRepository ?? new AgeRatingRepository(_context);
        public IGenreRepository GenreRepository => _genreRepository ?? new GenreRepository(_context);

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}