using CinemaAPI.Core.CustomEntities;
using CinemaAPI.Core.Entities;
using CinemaAPI.Core.Interfaces;
using CinemaAPI.Core.Options;
using CinemaAPI.Core.QueryFilters;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace CinemaAPI.Core.Services
{
    public class FilmService : IFilmService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public FilmService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public PagedList<Film> GetFilms(FilmQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;

            var films = _unitOfWork.FilmRepository.GetAll();

            var pagedGenres = PagedList<Film>.Create(films, filters.PageNumber, filters.PageSize);
            return pagedGenres;
        }


        public async Task<Film> GetFilm(int id)
        {
            //We can add business logic here :)
            return await _unitOfWork.FilmRepository.GetById(id);
        }

        public async Task AddFilm(Film film)
        {
            //We can add business logic here :)
            await _unitOfWork.FilmRepository.Add(film);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateFilm(Film film)
        {
            //We can add business logic here :)
            _unitOfWork.FilmRepository.Update(film);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveFilm(int id)
        {
            //We can add business logic here :)
            await _unitOfWork.FilmRepository.Delete(id);
            _unitOfWork.SaveChanges();
            return true;
        }

    }
}
