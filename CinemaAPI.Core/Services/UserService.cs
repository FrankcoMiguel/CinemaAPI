using CinemaAPI.Core.Entities;
using CinemaAPI.Core.Interfaces;
using System.Threading.Tasks;

namespace CinemaAPI.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<User> GetLoginByCredentials(UserLogin login)
        {
            return await _unitOfWork.UserRepository.GetLoginByCredentials(login);
        }

        public async Task RegisterUser(User user)
        {
            await _unitOfWork.UserRepository.Add(user);
            await _unitOfWork.SaveChangesAsync();
        }

    }
}
