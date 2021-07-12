using CinemaAPI.Core.CustomEntities;
using CinemaAPI.Core.Entities;
using CinemaAPI.Core.Interfaces;
using CinemaAPI.Core.Options;
using CinemaAPI.Core.QueryFilters;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace CinemaAPI.Core.Services
{
    public class RatingService : IRatingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public RatingService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public PagedList<Rating> GetRatings(GenreQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;

            var ratings = _unitOfWork.RatingRepository.GetAll();

            var pagedRatings = PagedList<Rating>.Create(ratings, filters.PageNumber, filters.PageSize);
            return pagedRatings;
        }


        public async Task<Rating> GetRating(int id)
        {
            //We can add business logic here :)
            return await _unitOfWork.RatingRepository.GetById(id);
        }

        public async Task AddRating(Rating rating)
        {
            //We can add business logic here :)
            await _unitOfWork.RatingRepository.Add(rating);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateRating(Rating rating)
        {
            //We can add business logic here :)
            _unitOfWork.RatingRepository.Update(rating);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveRating(int id)
        {
            //We can add business logic here :)
            await _unitOfWork.RatingRepository.Delete(id);
            _unitOfWork.SaveChanges();
            return true;
        }

    }
}
