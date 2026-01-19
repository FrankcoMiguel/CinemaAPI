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
    public class FilmServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IFilmRepository> _filmRepositoryMock;
        private readonly Mock<IOptions<PaginationOptions>> _paginationOptionsMock;
        private readonly FilmService _filmService;
        private readonly PaginationOptions _paginationOptions;

        public FilmServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _filmRepositoryMock = new Mock<IFilmRepository>();
            _paginationOptionsMock = new Mock<IOptions<PaginationOptions>>();
            
            _paginationOptions = new PaginationOptions { DefaultPageNumber = 1, DefaultPageSize = 10 };
            _paginationOptionsMock.Setup(x => x.Value).Returns(_paginationOptions);
            
            _unitOfWorkMock.Setup(u => u.FilmRepository).Returns(_filmRepositoryMock.Object);
            
            _filmService = new FilmService(_unitOfWorkMock.Object, _paginationOptionsMock.Object);
        }

        [Fact]
        public void GetFilms_ReturnsPagedList_WhenValidRequest()
        {
            // Arrange
            var films = TestData.GetFilms();
            var filters = new FilmQueryFilter { PageNumber = 1, PageSize = 10 };
            _filmRepositoryMock.Setup(r => r.GetAll()).Returns(films);
            
            // Act
            var result = _filmService.GetFilms(filters);
            
            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<PagedList<Film>>();
            _filmRepositoryMock.Verify(r => r.GetAll(), Times.Once);
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
            _filmRepositoryMock.Verify(r => r.GetById(filmId), Times.Once);
        }
        
        [Fact]
        public async Task GetFilm_ReturnsNull_WhenFilmDoesNotExist()
        {
            // Arrange
            var filmId = 99;
            _filmRepositoryMock.Setup(r => r.GetById(filmId)).ReturnsAsync((Film)null);
            
            // Act
            var result = await _filmService.GetFilm(filmId);
            
            // Assert
            result.Should().BeNull();
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

        [Fact]
        public async Task UpdateFilm_ReturnsTrue_WhenValidFilm()
        {
            // Arrange
            var film = TestData.GetFilm(1);
            _unitOfWorkMock.Setup(u => u.SaveChangesAsync()).Returns(Task.CompletedTask);
            
            // Act
            var result = await _filmService.UpdateFilm(film);
            
            // Assert
            result.Should().BeTrue();
            _filmRepositoryMock.Verify(r => r.Update(film), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task RemoveFilm_ReturnsTrue_WhenValidId()
        {
            // Arrange
            var filmId = 1;
            _filmRepositoryMock.Setup(r => r.Delete(filmId)).Returns(Task.CompletedTask);
            
            // Act
            var result = await _filmService.RemoveFilm(filmId);
            
            // Assert
            result.Should().BeTrue();
            _filmRepositoryMock.Verify(r => r.Delete(filmId), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChanges(), Times.Once);
        }
    }
}