using CinemaAPI.Core.CustomEntities;
using CinemaAPI.Core.Entities;
using CinemaAPI.Core.Interfaces;
using CinemaAPI.Core.QueryFilters;
using System.Threading.Tasks;

namespace CinemaAPI.Core.Services
{
    public class GenreService : IGenreService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GenreService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public PagedList<Genre> GetGenres(GenreQueryFilter filters)
        {
            var genres = _unitOfWork.GenreRepository.GetAll();

            var pagedGenres = PagedList<Genre>.Create(genres, filters.PageNumber, filters.PageSize);
            return pagedGenres;
        }


        public async Task<Genre> GetGenre(int id)
        {
            //We can add business logic here :)
            return await _unitOfWork.GenreRepository.GetById(id);
        }

        public async Task AddGenre(Genre genre)
        {
            //We can add business logic here :)
            await _unitOfWork.GenreRepository.Add(genre);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateGenre(Genre genre)
        {
            //We can add business logic here :)
            _unitOfWork.GenreRepository.Update(genre);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveGenre(int id)
        {
            //We can add business logic here :)
            await _unitOfWork.GenreRepository.Delete(id);
            _unitOfWork.SaveChanges();
            return true;
        }

    }
}
