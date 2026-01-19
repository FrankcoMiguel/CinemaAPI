using CinemaAPI.Infrastructure.Data;
using CinemaAPI.UnitTests.Helpers;
using FluentAssertions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CinemaAPI.UnitTests.Integration
{
    public class DatabaseIntegrationTests
    {
        [Fact]
        public async Task Database_ShouldSaveAndRetrieveFilms()
        {
            // Arrange
            using var context = TestHelpers.GetInMemoryContext();
            var films = TestData.GetFilms();

            // Act
            await context.Film.AddRangeAsync(films);
            await context.SaveChangesAsync();

            // Assert
            var savedFilms = context.Film.ToList();
            savedFilms.Should().HaveCount(2);
            savedFilms.Should().Contain(f => f.Title == "Test Movie 1");
            savedFilms.Should().Contain(f => f.Title == "Test Movie 2");
        }

        [Fact]
        public async Task Database_ShouldHandleRelationships()
        {
            // Arrange
            using var context = TestHelpers.GetInMemoryContext();
            var ratings = TestData.GetRatings();
            var films = TestData.GetFilms();

            // Act
            await context.Rating.AddRangeAsync(ratings);
            await context.SaveChangesAsync();

            films[0].RatingId = ratings[0].Id;
            films[1].RatingId = ratings[1].Id;

            await context.Film.AddRangeAsync(films);
            await context.SaveChangesAsync();

            // Assert
            var savedFilm = context.Film
                .Where(f => f.Id == films[0].Id)
                .FirstOrDefault();

            savedFilm.Should().NotBeNull();
            savedFilm.RatingId.Should().Be(ratings[0].Id);
        }

        [Fact]
        public async Task Database_ShouldHandleCascadeOperations()
        {
            // Arrange
            using var context = TestHelpers.GetInMemoryContext();
            var persons = TestData.GetPersons();
            var films = TestData.GetFilms();
            var occupations = TestData.GetOccupations();

            // Act
            await context.Person.AddRangeAsync(persons);
            await context.Film.AddRangeAsync(films);
            await context.Occupation.AddRangeAsync(occupations);
            await context.SaveChangesAsync();

            var casting = new CinemaAPI.Core.Entities.Casting
            {
                PersonId = persons[0].Id,
                FilmId = films[0].Id,
                OccupationId = occupations[0].Id
            };

            await context.Casting.AddAsync(casting);
            await context.SaveChangesAsync();

            // Assert
            var savedCasting = context.Casting.FirstOrDefault();
            savedCasting.Should().NotBeNull();
            savedCasting.PersonId.Should().Be(persons[0].Id);
            savedCasting.FilmId.Should().Be(films[0].Id);
            savedCasting.OccupationId.Should().Be(occupations[0].Id);
        }
    }
}