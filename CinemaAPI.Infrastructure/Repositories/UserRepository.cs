using CinemaAPI.Core.Entities;
using CinemaAPI.Core.Interfaces;
using CinemaAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CinemaAPI.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(CinemaAPIContext context) : base(context) { }


        public async Task<User> GetLoginByCredentials(UserLogin login)
        {
            var user = await _entities.FirstOrDefaultAsync(x => x.Username.Equals(login.Username));
            return user;
        }

    }
}
