using CinemaAPI.Core.Entities;
using CinemaAPI.Core.Interfaces;
using CinemaAPI.Core.QueryFilters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CinemaAPI.Core.Services
{
    public class AgeRatingService : IAgeRatingService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AgeRatingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<AgeRating> GetAgeRatings(AgeRatingQueryFilter filters)
        {
            var ageRatings = _unitOfWork.AgeRatingRepository.GetAll();
            return ageRatings;
        }


        public async Task<AgeRating> GetAgeRating(int id)
        {
            //We can add business logic here :)
            return await _unitOfWork.AgeRatingRepository.GetById(id);
        }

        public async Task AddAgeRating(AgeRating ageRating)
        {
            //We can add business logic here :)
            await _unitOfWork.AgeRatingRepository.Add(ageRating);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateAgeRating(AgeRating ageRating)
        {
            //We can add business logic here :)
            _unitOfWork.AgeRatingRepository.Update(ageRating);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveAgeRating(int id)
        {
            //We can add business logic here :)
            await _unitOfWork.AgeRatingRepository.Delete(id);
            _unitOfWork.SaveChanges();
            return true;
        }

    }
}
