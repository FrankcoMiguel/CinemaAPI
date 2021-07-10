using CinemaAPI.Core.CustomEntities;
using CinemaAPI.Core.Entities;
using CinemaAPI.Core.Interfaces;
using CinemaAPI.Core.Options;
using CinemaAPI.Core.QueryFilters;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace CinemaAPI.Core.Services
{
    public class GenreService : IGenreService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public GenreService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public PagedList<Genre> GetGenres(GenreQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;

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
