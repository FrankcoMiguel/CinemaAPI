using CinemaAPI.Core.CustomEntities;
using CinemaAPI.Core.Entities;
using CinemaAPI.Core.QueryFilters;
using System.Threading.Tasks;

namespace CinemaAPI.Core.Interfaces
{
    public interface IPersonService
    {
        PagedList<Person> GetPersons(GenreQueryFilter filters);
        Task<Person> GetPerson(int id);
        Task AddPerson(Person person);
        Task<bool> UpdatePerson(Person person);
        Task<bool> RemovePerson(int id);
    }
}
