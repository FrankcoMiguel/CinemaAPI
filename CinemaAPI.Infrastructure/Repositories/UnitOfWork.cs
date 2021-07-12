using CinemaAPI.Core.Interfaces;
using CinemaAPI.Infrastructure.Data;
using System.Threading.Tasks;

namespace CinemaAPI.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CinemaAPIContext _context;

        private readonly IFilmRepository _filmRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IOccupationRepository _occupationRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IRatingRepository _ratingRepository;



        public UnitOfWork(CinemaAPIContext context)
        {
            _context = context;
        }

        public IFilmRepository FilmRepository => _filmRepository ?? new FilmRepository(_context);
        public IGenreRepository GenreRepository => _genreRepository ?? new GenreRepository(_context);
        public IOccupationRepository OccupationRepository => _occupationRepository ?? new OccupationRepository(_context);
        public IPersonRepository PersonRepository => _personRepository ?? new PersonRepository(_context);
        public IRatingRepository RatingRepository => _ratingRepository ?? new RatingRepository(_context);

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