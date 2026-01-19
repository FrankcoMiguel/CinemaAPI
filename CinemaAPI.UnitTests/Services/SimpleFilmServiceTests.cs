using CinemaAPI.Core.Services;
using CinemaAPI.Core.Entities;
using CinemaAPI.Core.Interfaces;
using CinemaAPI.Core.Options;
using CinemaAPI.Core.QueryFilters;
using CinemaAPI.Core.CustomEntities;
using CinemaAPI.UnitTests.TestHelpers;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace CinemaAPI.UnitTests.Services
{
    public class SimpleFilmServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IFilmRepository> _filmRepositoryMock;
        private readonly Mock<IOptions<PaginationOptions>> _paginationOptionsMock;
        private readonly FilmService _filmService;

        public SimpleFilmServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _filmRepositoryMock = new Mock<IFilmRepository>();
            _paginationOptionsMock = new Mock<IOptions<PaginationOptions>>();
            
            var paginationOptions = new PaginationOptions { DefaultPageNumber = 1, DefaultPageSize = 10 };
            _paginationOptionsMock.Setup(x => x.Value).Returns(paginationOptions);
            
            _unitOfWorkMock.Setup(u => u.FilmRepository).Returns(_filmRepositoryMock.Object);
            
            _filmService = new FilmService(_unitOfWorkMock.Object, _paginationOptionsMock.Object);
        }

        [Fact]
        public async Task GetFilm_ReturnsFilm_WhenFilmExists()
        {
            // Arrange
            var filmId = 1;
            var film = TestData.GetFilm(filmId);
            _filmRepositoryMock.Setup(r => r.GetById(filmId)).ReturnsAsync(film);
            
            // Act
            var result = await _filmService.GetFilm(filmId);
            
            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(filmId);
        }
        
        [Fact]
        public async Task AddFilm_CallsRepository_WhenValidFilm()
        {
            // Arrange
            var film = TestData.GetFilm(1);
            _filmRepositoryMock.Setup(r => r.Add(film)).Returns(Task.CompletedTask);
            _unitOfWorkMock.Setup(u => u.SaveChangesAsync()).Returns(Task.CompletedTask);
            
            // Act
            await _filmService.AddFilm(film);
            
            // Assert
            _filmRepositoryMock.Verify(r => r.Add(film), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }
    }
}