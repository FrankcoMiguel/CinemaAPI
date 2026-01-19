using CinemaAPI.Api;
using CinemaAPI.Core.DTOs;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace CinemaAPI.IntegrationTests.Controllers
{
    public class FilmControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Program> _factory;
        private readonly JsonSerializerOptions _jsonOptions;

        public FilmControllerIntegrationTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _factory.SeedDatabase(); // Seed the database for each test class
            _client = _factory.CreateClient();
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        [Fact]
        public async Task GetFilms_ReturnsSuccessAndCorrectContentType()
        {
            // Act
            var response = await _client.GetAsync("/api/films");

            // Assert
            response.EnsureSuccessStatusCode();
            response.Content.Headers.ContentType.ToString()
                .Should().Contain("application/json");
        }

        [Fact]
        public async Task GetFilms_ReturnsFilms()
        {
            // Act
            var response = await _client.GetAsync("/api/films");
            var content = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            content.Should().NotBeEmpty();
        }

        [Fact]
        public async Task GetFilmById_ExistingId_ReturnsFilm()
        {
            // Arrange
            var filmId = 1;

            // Act
            var response = await _client.GetAsync($"/api/films/{filmId}");
            var content = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            content.Should().NotBeEmpty();
            content.Should().Contain("Test Movie 1");
        }

        [Fact]
        public async Task GetFilmById_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            var filmId = 999;

            // Act
            var response = await _client.GetAsync($"/api/films/{filmId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task PostFilm_ValidFilm_ReturnsCreated()
        {
            // Arrange
            var newFilm = new FilmDTO
            {
                Title = "New Test Movie",
                Synopsis = "A new test movie for integration testing",
                ReleaseYear = 2023,
                RunningTime = 150,
                Language = "English"
            };

            var json = JsonSerializer.Serialize(newFilm, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/films", content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task PostFilm_InvalidFilm_ReturnsBadRequest()
        {
            // Arrange - Film without required title
            var invalidFilm = new FilmDTO
            {
                Synopsis = "A movie without title",
                ReleaseYear = 2023,
                RunningTime = 150,
                Language = "English"
            };

            var json = JsonSerializer.Serialize(invalidFilm, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/films", content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task PutFilm_ExistingId_ReturnsNoContent()
        {
            // Arrange
            var filmId = 1;
            var updatedFilm = new FilmDTO
            {
                Id = filmId,
                Title = "Updated Test Movie",
                Synopsis = "Updated synopsis",
                ReleaseYear = 2022,
                RunningTime = 140,
                Language = "English"
            };

            var json = JsonSerializer.Serialize(updatedFilm, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync($"/api/films/{filmId}", content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task DeleteFilm_ExistingId_ReturnsNoContent()
        {
            // Arrange
            var filmId = 2;

            // Act
            var response = await _client.DeleteAsync($"/api/films/{filmId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task DeleteFilm_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            var filmId = 999;

            // Act
            var response = await _client.DeleteAsync($"/api/films/{filmId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}