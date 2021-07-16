using CinemaAPI.Core.CustomEntities;
using CinemaAPI.Core.Entities;
using CinemaAPI.Core.Interfaces;
using CinemaAPI.Core.Options;
using CinemaAPI.Core.QueryFilters;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace CinemaAPI.Core.Services
{
    public class PersonService : IPersonService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public PersonService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public PagedList<Person> GetPersons(GenreQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;

            var persons = _unitOfWork.PersonRepository.GetAll();

            var pagedPersons = PagedList<Person>.Create(persons, filters.PageNumber, filters.PageSize);
            return pagedPersons;
        }


        public async Task<Person> GetPerson(int id)
        {
            //We can add business logic here :)
            return await _unitOfWork.PersonRepository.GetById(id);
        }

        public async Task AddPerson(Person person)
        {
            //We can add business logic here :)
            await _unitOfWork.PersonRepository.Add(person);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdatePerson(Person person)
        {
            //We can add business logic here :)
            _unitOfWork.PersonRepository.Update(person);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemovePerson(int id)
        {
            //We can add business logic here :)
            await _unitOfWork.PersonRepository.Delete(id);
            _unitOfWork.SaveChanges();
            return true;
        }

    }
}
