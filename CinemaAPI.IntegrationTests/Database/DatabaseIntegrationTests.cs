using CinemaAPI.Api;
using CinemaAPI.Infrastructure.Data;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CinemaAPI.IntegrationTests.Database
{
    public class DatabaseIntegrationTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;

        public DatabaseIntegrationTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _factory.SeedDatabase(); // Seed the database for each test class
        }

        [Fact]
        public async Task Database_ShouldBeSeededWithTestData()
        {
            // Arrange
            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<CinemaAPIContext>();

            // Act & Assert
            var films = context.Film.ToList();
            films.Should().NotBeEmpty();
            films.Should().HaveCount(2);
            films.First().Title.Should().Be("Test Movie 1");

            var genres = context.Genre.ToList();
            genres.Should().NotBeEmpty();
            genres.Should().HaveCount(2);
            genres.Should().Contain(g => g.Description == "Action");

            var users = context.User.ToList();
            users.Should().NotBeEmpty();
            users.Should().HaveCount(2);
            users.Should().Contain(u => u.Username == "johndoe");
        }

        [Fact]
        public async Task Database_CanCreateAndRetrieveFilm()
        {
            // Arrange
            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<CinemaAPIContext>();

            var newFilm = new Core.Entities.Film
            {
                Title = "Integration Test Movie",
                Synopsis = "A movie created during integration testing",
                ReleaseYear = 2024,
                RunningTime = 120,
                Language = "English",
                PictureReference = "integration_test.jpg"
            };

            // Act
            context.Film.Add(newFilm);
            await context.SaveChangesAsync();

            // Assert
            var retrievedFilm = context.Film.FirstOrDefault(f => f.Title == "Integration Test Movie");
            retrievedFilm.Should().NotBeNull();
            retrievedFilm.Synopsis.Should().Be("A movie created during integration testing");
            retrievedFilm.ReleaseYear.Should().Be(2024);
        }

        [Fact]
        public async Task Database_CanUpdateFilmData()
        {
            // Arrange
            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<CinemaAPIContext>();

            var film = context.Film.First();
            var originalTitle = film.Title;

            // Act
            film.Title = "Updated Title";
            await context.SaveChangesAsync();

            // Assert
            var updatedFilm = context.Film.Find(film.Id);
            updatedFilm.Title.Should().Be("Updated Title");
            updatedFilm.Title.Should().NotBe(originalTitle);
        }

        [Fact]
        public async Task Database_CanDeleteFilm()
        {
            // Arrange
            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<CinemaAPIContext>();

            var filmToDelete = new Core.Entities.Film
            {
                Title = "Film To Delete",
                Synopsis = "This film will be deleted",
                ReleaseYear = 2023,
                RunningTime = 90,
                Language = "English"
            };

            context.Film.Add(filmToDelete);
            await context.SaveChangesAsync();
            var filmId = filmToDelete.Id;

            // Act
            context.Film.Remove(filmToDelete);
            await context.SaveChangesAsync();

            // Assert
            var deletedFilm = context.Film.Find(filmId);
            deletedFilm.Should().BeNull();
        }

        [Fact]
        public async Task Database_CanHandleConcurrentOperations()
        {
            // Arrange
            using var scope1 = _factory.Services.CreateScope();
            using var scope2 = _factory.Services.CreateScope();
            
            var context1 = scope1.ServiceProvider.GetRequiredService<CinemaAPIContext>();
            var context2 = scope2.ServiceProvider.GetRequiredService<CinemaAPIContext>();

            // Act
            var film1 = new Core.Entities.Film
            {
                Title = "Concurrent Film 1",
                Synopsis = "First concurrent operation",
                ReleaseYear = 2024,
                RunningTime = 100,
                Language = "English"
            };

            var film2 = new Core.Entities.Film
            {
                Title = "Concurrent Film 2",
                Synopsis = "Second concurrent operation",
                ReleaseYear = 2024,
                RunningTime = 110,
                Language = "Spanish"
            };

            context1.Film.Add(film1);
            context2.Film.Add(film2);

            await Task.WhenAll(
                context1.SaveChangesAsync(),
                context2.SaveChangesAsync()
            );

            // Assert
            using var verificationScope = _factory.Services.CreateScope();
            var verificationContext = verificationScope.ServiceProvider.GetRequiredService<CinemaAPIContext>();
            
            var films = verificationContext.Film
                .Where(f => f.Title.StartsWith("Concurrent Film"))
                .ToList();

            films.Should().HaveCount(2);
            films.Should().Contain(f => f.Title == "Concurrent Film 1");
            films.Should().Contain(f => f.Title == "Concurrent Film 2");
        }
    }
}