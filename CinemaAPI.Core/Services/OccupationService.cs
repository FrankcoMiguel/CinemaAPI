using CinemaAPI.Core.CustomEntities;
using CinemaAPI.Core.Entities;
using CinemaAPI.Core.Interfaces;
using CinemaAPI.Core.Options;
using CinemaAPI.Core.QueryFilters;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace CinemaAPI.Core.Services
{
    public class OccupationService : IOccupationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public OccupationService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public PagedList<Occupation> GetOccupations(FilmQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;

            var occupations = _unitOfWork.OccupationRepository.GetAll();

            var pagedOccupations = PagedList<Occupation>.Create(occupations, filters.PageNumber, filters.PageSize);
            return pagedOccupations;
        }

        public async Task<Occupation> GetOccupation(int id)
        {
            //We can add business logic here :)
            return await _unitOfWork.OccupationRepository.GetById(id);
        }

        public async Task AddOccupation(Occupation occupation)
        {
            //We can add business logic here :)
            await _unitOfWork.OccupationRepository.Add(occupation);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateOccupation(Occupation occupation)
        {
            //We can add business logic here :)
            _unitOfWork.OccupationRepository.Update(occupation);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveOccupation(int id)
        {
            //We can add business logic here :)
            await _unitOfWork.OccupationRepository.Delete(id);
            _unitOfWork.SaveChanges();
            return true;
        }

    }
}
