using CinemaAPI.Core.Entities;
using System.Threading.Tasks;

namespace CinemaAPI.Core.Interfaces
{
    public interface IUserService
    {
        Task<User> GetLoginByCredentials(UserLogin login);
        Task RegisterUser(User user);
    }
}
