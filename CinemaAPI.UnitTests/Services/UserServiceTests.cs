using CinemaAPI.Core.Services;
using CinemaAPI.Core.Entities;
using CinemaAPI.Core.Interfaces;
using CinemaAPI.UnitTests.TestHelpers;
using FluentAssertions;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace CinemaAPI.UnitTests.Services
{
    public class UserServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _userRepositoryMock = new Mock<IUserRepository>();
            
            _unitOfWorkMock.Setup(u => u.UserRepository).Returns(_userRepositoryMock.Object);
            
            _userService = new UserService(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task GetLoginByCredentials_ReturnsUser_WhenCredentialsValid()
        {
            // Arrange
            var userLogin = new UserLogin
            {
                Username = "testuser",
                Password = "password"
            };
            var user = TestData.GetUser(1);
            _userRepositoryMock.Setup(r => r.GetLoginByCredentials(userLogin)).ReturnsAsync(user);
            
            // Act
            var result = await _userService.GetLoginByCredentials(userLogin);
            
            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(user.Id);
            _userRepositoryMock.Verify(r => r.GetLoginByCredentials(userLogin), Times.Once);
        }
        
        [Fact]
        public async Task GetLoginByCredentials_ReturnsNull_WhenCredentialsInvalid()
        {
            // Arrange
            var userLogin = new UserLogin
            {
                Username = "invaliduser",
                Password = "wrongpassword"
            };
            _userRepositoryMock.Setup(r => r.GetLoginByCredentials(userLogin)).ReturnsAsync((User)null);
            
            // Act
            var result = await _userService.GetLoginByCredentials(userLogin);
            
            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task RegisterUser_CallsRepository_WhenValidUser()
        {
            // Arrange
            var user = TestData.GetUser(1);
            _userRepositoryMock.Setup(r => r.Add(user)).Returns(Task.CompletedTask);
            _unitOfWorkMock.Setup(u => u.SaveChangesAsync()).Returns(Task.CompletedTask);
            
            // Act
            await _userService.RegisterUser(user);
            
            // Assert
            _userRepositoryMock.Verify(r => r.Add(user), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }
    }
}