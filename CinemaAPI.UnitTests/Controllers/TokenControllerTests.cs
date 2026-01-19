using CinemaAPI.Api.Controllers;
using CinemaAPI.Core.Entities;
using CinemaAPI.Core.Interfaces;
using CinemaAPI.Infrastructure.Interfaces;
using CinemaAPI.UnitTests.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace CinemaAPI.UnitTests.Controllers
{
    public class TokenControllerTests
    {
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly Mock<IUserService> _mockUserService;
        private readonly Mock<IPasswordService> _mockPasswordService;
        private readonly TokenController _controller;

        public TokenControllerTests()
        {
            _mockConfiguration = new Mock<IConfiguration>();
            _mockUserService = new Mock<IUserService>();
            _mockPasswordService = new Mock<IPasswordService>();

            // Setup configuration
            _mockConfiguration.Setup(c => c["Authentication:SecretKey"]).Returns("cinemaapicinemaapicinemaapi12243234$$");
            _mockConfiguration.Setup(c => c["Authentication:Issuer"]).Returns("http://localhost:44389/");
            _mockConfiguration.Setup(c => c["Authentication:Audience"]).Returns("http://localhost:44389/");

            _controller = new TokenController(_mockConfiguration.Object, _mockUserService.Object, _mockPasswordService.Object);
        }

        [Fact]
        public async Task Authentication_WithValidCredentials_ShouldReturnOkWithToken()
        {
            // Arrange
            var userLogin = new UserLogin { Username = "admin", Password = "password" };
            var user = TestData.GetUsers()[0];

            _mockUserService.Setup(s => s.GetLoginByCredentials(userLogin)).ReturnsAsync(user);
            _mockPasswordService.Setup(s => s.Check(user.Password, userLogin.Password)).Returns(true);

            // Act
            var result = await _controller.Authentication(userLogin);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().NotBeNull();
            
            // Check if the result contains a token property
            var resultValue = okResult.Value;
            var tokenProperty = resultValue.GetType().GetProperty("token");
            tokenProperty.Should().NotBeNull();
            tokenProperty.GetValue(resultValue).Should().NotBeNull();
        }

        [Fact]
        public async Task Authentication_WithInvalidCredentials_ShouldReturnNotFound()
        {
            // Arrange
            var userLogin = new UserLogin { Username = "invalid", Password = "invalid" };

            _mockUserService.Setup(s => s.GetLoginByCredentials(userLogin)).ReturnsAsync((User)null);

            // Act
            var result = await _controller.Authentication(userLogin);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Authentication_WithValidUserButInvalidPassword_ShouldReturnNotFound()
        {
            // Arrange
            var userLogin = new UserLogin { Username = "admin", Password = "wrongpassword" };
            var user = TestData.GetUsers()[0];

            _mockUserService.Setup(s => s.GetLoginByCredentials(userLogin)).ReturnsAsync(user);
            _mockPasswordService.Setup(s => s.Check(user.Password, userLogin.Password)).Returns(false);

            // Act
            var result = await _controller.Authentication(userLogin);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}