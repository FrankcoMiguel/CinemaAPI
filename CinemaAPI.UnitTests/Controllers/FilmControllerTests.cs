using AutoMapper;
using CinemaAPI.Api.Controllers;
using CinemaAPI.Api.Responses;
using CinemaAPI.Core.DTOs;
using CinemaAPI.Core.Entities;
using CinemaAPI.Core.Interfaces;
using CinemaAPI.UnitTests.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CinemaAPI.UnitTests.Controllers
{
    public class FilmControllerTests
    {
        private readonly Mock<IFilmService> _mockFilmService;
        private readonly IMapper _mapper;
        private readonly FilmController _controller;

        public FilmControllerTests()
        {
            _mockFilmService = new Mock<IFilmService>();
            _mapper = TestHelpers.GetMapper();

            _controller = new FilmController(_mockFilmService.Object, _mapper, null);
        }

        [Fact]
        public async Task GetFilm_WithValidId_ShouldReturnOkResult()
        {
            // Arrange
            var film = TestData.GetFilms()[0];
            _mockFilmService.Setup(s => s.GetFilm(1)).ReturnsAsync(film);

            // Act
            var result = await _controller.GetFilm(1);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            var response = okResult.Value as ApiResponse<FilmDTO>;
            response.Should().NotBeNull();
            response.Data.Title.Should().Be("Test Movie 1");
        }

        [Fact]
        public async Task GetFilm_WithInvalidId_ShouldReturnNotFound()
        {
            // Arrange
            _mockFilmService.Setup(s => s.GetFilm(999)).ReturnsAsync((Film)null);

            // Act
            var result = await _controller.GetFilm(999);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Post_WithValidFilm_ShouldReturnOkResult()
        {
            // Arrange
            var filmDto = new FilmDTO
            {
                Title = "New Film",
                Synopsis = "A new film",
                ReleaseYear = 2024,
                RunningTime = 110,
                Language = "English"
            };

            _mockFilmService.Setup(s => s.AddFilm(It.IsAny<Film>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Post(filmDto);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            var response = okResult.Value as ApiResponse<FilmDTO>;
            response.Should().NotBeNull();
            response.Data.Title.Should().Be("New Film");
            
            _mockFilmService.Verify(s => s.AddFilm(It.IsAny<Film>()), Times.Once);
        }

        [Fact]
        public async Task Put_WithValidIdAndFilm_ShouldReturnOkResult()
        {
            // Arrange
            var filmDto = new FilmDTO
            {
                Id = 1,
                Title = "Updated Film",
                Synopsis = "Updated synopsis",
                ReleaseYear = 2024,
                RunningTime = 130,
                Language = "English"
            };

            _mockFilmService.Setup(s => s.UpdateFilm(It.IsAny<Film>())).ReturnsAsync(true);

            // Act
            var result = await _controller.Put(1, filmDto);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            var response = okResult.Value as ApiResponse<bool>;
            response.Should().NotBeNull();
            response.Data.Should().BeTrue();
            
            _mockFilmService.Verify(s => s.UpdateFilm(It.Is<Film>(f => f.Id == 1)), Times.Once);
        }

        [Fact]
        public async Task Delete_WithValidId_ShouldReturnOkResult()
        {
            // Arrange
            _mockFilmService.Setup(s => s.RemoveFilm(1)).ReturnsAsync(true);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            var response = okResult.Value as ApiResponse<bool>;
            response.Should().NotBeNull();
            response.Data.Should().BeTrue();
            
            _mockFilmService.Verify(s => s.RemoveFilm(1), Times.Once);
        }
    }
}