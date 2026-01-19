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
    public class GenreControllerTests
    {
        private readonly Mock<IGenreService> _mockGenreService;
        private readonly IMapper _mapper;
        private readonly GenreController _controller;

        public GenreControllerTests()
        {
            _mockGenreService = new Mock<IGenreService>();
            _mapper = TestHelpers.GetMapper();

            _controller = new GenreController(_mockGenreService.Object, _mapper, null);
        }

        [Fact]
        public async Task GetGenre_WithValidId_ShouldReturnOkResult()
        {
            // Arrange
            var genre = TestData.GetGenres()[0];
            _mockGenreService.Setup(s => s.GetGenre(1)).ReturnsAsync(genre);

            // Act
            var result = await _controller.GetGenre(1);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            var response = okResult.Value as ApiResponse<GenreDTO>;
            response.Should().NotBeNull();
            response.Data.Name.Should().Be("Action");
        }

        [Fact]
        public async Task GetGenre_WithInvalidId_ShouldReturnNotFound()
        {
            // Arrange
            _mockGenreService.Setup(s => s.GetGenre(999)).ReturnsAsync((Genre)null);

            // Act
            var result = await _controller.GetGenre(999);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Post_WithValidGenre_ShouldReturnOkResult()
        {
            // Arrange
            var genreDto = new GenreDTO
            {
                Name = "Horror",
                Description = "Horror movies"
            };

            _mockGenreService.Setup(s => s.AddGenre(It.IsAny<Genre>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Post(genreDto);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            var response = okResult.Value as ApiResponse<GenreDTO>;
            response.Should().NotBeNull();
            response.Data.Name.Should().Be("Horror");
            
            _mockGenreService.Verify(s => s.AddGenre(It.IsAny<Genre>()), Times.Once);
        }

        [Fact]
        public async Task Put_WithValidIdAndGenre_ShouldReturnOkResult()
        {
            // Arrange
            var genreDto = new GenreDTO
            {
                Id = 1,
                Name = "Updated Genre",
                Description = "Updated description"
            };

            _mockGenreService.Setup(s => s.UpdateGenre(It.IsAny<Genre>())).ReturnsAsync(true);

            // Act
            var result = await _controller.Put(1, genreDto);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            var response = okResult.Value as ApiResponse<bool>;
            response.Should().NotBeNull();
            response.Data.Should().BeTrue();
            
            _mockGenreService.Verify(s => s.UpdateGenre(It.Is<Genre>(g => g.Id == 1)), Times.Once);
        }

        [Fact]
        public async Task Delete_WithValidId_ShouldReturnOkResult()
        {
            // Arrange
            _mockGenreService.Setup(s => s.RemoveGenre(1)).ReturnsAsync(true);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            var response = okResult.Value as ApiResponse<bool>;
            response.Should().NotBeNull();
            response.Data.Should().BeTrue();
            
            _mockGenreService.Verify(s => s.RemoveGenre(1), Times.Once);
        }
    }
}