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
    public class GenreControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Program> _factory;
        private readonly JsonSerializerOptions _jsonOptions;

        public GenreControllerIntegrationTests(CustomWebApplicationFactory<Program> factory)
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
        public async Task GetGenres_ReturnsSuccessAndCorrectContentType()
        {
            // Act
            var response = await _client.GetAsync("/api/genres");

            // Assert
            response.EnsureSuccessStatusCode();
            response.Content.Headers.ContentType.ToString()
                .Should().Contain("application/json");
        }

        [Fact]
        public async Task GetGenres_ReturnsGenres()
        {
            // Act
            var response = await _client.GetAsync("/api/genres");
            var content = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            content.Should().NotBeEmpty();
            content.Should().Contain("Action");
            content.Should().Contain("Comedy");
        }

        [Fact]
        public async Task GetGenreById_ExistingId_ReturnsGenre()
        {
            // Arrange
            var genreId = 1;

            // Act
            var response = await _client.GetAsync($"/api/genres/{genreId}");
            var content = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            content.Should().NotBeEmpty();
            content.Should().Contain("Action");
        }

        [Fact]
        public async Task GetGenreById_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            var genreId = 999;

            // Act
            var response = await _client.GetAsync($"/api/genres/{genreId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task PostGenre_ValidGenre_ReturnsCreated()
        {
            // Arrange
            var newGenre = new GenreDTO
            {
                Description = "Horror"
            };

            var json = JsonSerializer.Serialize(newGenre, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/genres", content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task PostGenre_InvalidGenre_ReturnsBadRequest()
        {
            // Arrange - Genre without description
            var invalidGenre = new GenreDTO();

            var json = JsonSerializer.Serialize(invalidGenre, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/genres", content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task PutGenre_ExistingId_ReturnsNoContent()
        {
            // Arrange
            var genreId = 1;
            var updatedGenre = new GenreDTO
            {
                Id = genreId,
                Description = "Updated Action"
            };

            var json = JsonSerializer.Serialize(updatedGenre, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync($"/api/genres/{genreId}", content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task DeleteGenre_ExistingId_ReturnsNoContent()
        {
            // Arrange
            var genreId = 2;

            // Act
            var response = await _client.DeleteAsync($"/api/genres/{genreId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}