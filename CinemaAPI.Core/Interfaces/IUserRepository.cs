using CinemaAPI.Core.Entities;
using System.Threading.Tasks;

namespace CinemaAPI.Core.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetLoginByCredentials(UserLogin login);
    }
}
