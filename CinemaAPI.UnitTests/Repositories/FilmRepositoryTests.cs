using CinemaAPI.Core.Entities;
using CinemaAPI.Infrastructure.Data;
using CinemaAPI.Infrastructure.Repositories;
using CinemaAPI.UnitTests.TestHelpers;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CinemaAPI.UnitTests.Repositories
{
    public class FilmRepositoryTests : IDisposable
    {
        private readonly CinemaAPIContext _context;
        private readonly FilmRepository _repository;

        public FilmRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<CinemaAPIContext>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
                .Options;

            _context = new CinemaAPIContext(options);
            _repository = new FilmRepository(_context);
        }

        [Fact]
        public void GetAll_ReturnsAllFilms()
        {
            // Arrange
            var film1 = TestData.GetFilm(1);
            var film2 = TestData.GetFilm(2);
            _context.Film.AddRange(film1, film2);
            _context.SaveChanges();

            // Act
            var result = _repository.GetAll();

            // Assert
            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetById_ReturnsFilm_WhenExists()
        {
            // Arrange
            var film = TestData.GetFilm(1);
            _context.Film.Add(film);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetById(film.Id);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(film.Id);
            result.Title.Should().Be(film.Title);
        }

        [Fact]
        public async Task GetById_ReturnsNull_WhenNotExists()
        {
            // Act
            var result = await _repository.GetById(999);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task Add_AddsFilm()
        {
            // Arrange
            var film = TestData.GetFilm(1);

            // Act
            await _repository.Add(film);
            await _context.SaveChangesAsync();

            // Assert
            var result = await _context.Film.FindAsync(film.Id);
            result.Should().NotBeNull();
            result.Title.Should().Be(film.Title);
        }

        [Fact]
        public async Task Update_UpdatesFilm()
        {
            // Arrange
            var film = TestData.GetFilm(1);
            _context.Film.Add(film);
            await _context.SaveChangesAsync();

            film.Title = "Updated Title";

            // Act
            _repository.Update(film);
            await _context.SaveChangesAsync();

            // Assert
            var result = await _context.Film.FindAsync(film.Id);
            result.Should().NotBeNull();
            result.Title.Should().Be("Updated Title");
        }

        [Fact]
        public async Task Delete_DeletesFilm()
        {
            // Arrange
            var film = TestData.GetFilm(1);
            _context.Film.Add(film);
            await _context.SaveChangesAsync();

            // Act
            await _repository.Delete(film.Id);
            await _context.SaveChangesAsync();

            // Assert
            var result = await _context.Film.FindAsync(film.Id);
            result.Should().BeNull();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}