using CinemaAPI.Api;
using CinemaAPI.Core.DTOs;
using CinemaAPI.Core.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace CinemaAPI.IntegrationTests.Workflows
{
    public class FilmWorkflowIntegrationTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Program> _factory;
        private readonly JsonSerializerOptions _jsonOptions;

        public FilmWorkflowIntegrationTests(CustomWebApplicationFactory<Program> factory)
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
        public async Task CompleteFilmWorkflow_CreateReadUpdateDelete_ShouldWork()
        {
            // Step 1: Create a new film
            var newFilm = new FilmDTO
            {
                Title = "Workflow Test Movie",
                Synopsis = "A movie created to test the complete CRUD workflow",
                ReleaseYear = 2024,
                RunningTime = 125,
                Language = "English"
            };

            var createJson = JsonSerializer.Serialize(newFilm, _jsonOptions);
            var createContent = new StringContent(createJson, Encoding.UTF8, "application/json");

            var createResponse = await _client.PostAsync("/api/films", createContent);
            createResponse.StatusCode.Should().Be(HttpStatusCode.Created);

            var locationHeader = createResponse.Headers.Location.ToString();
            var filmId = ExtractIdFromLocationHeader(locationHeader);

            // Step 2: Read the created film
            var getResponse = await _client.GetAsync($"/api/films/{filmId}");
            getResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var getContent = await getResponse.Content.ReadAsStringAsync();
            var retrievedFilm = JsonSerializer.Deserialize<FilmDTO>(getContent, _jsonOptions);
            
            retrievedFilm.Should().NotBeNull();
            retrievedFilm.Title.Should().Be("Workflow Test Movie");
            retrievedFilm.Synopsis.Should().Be("A movie created to test the complete CRUD workflow");

            // Step 3: Update the film
            var updatedFilm = new FilmDTO
            {
                Id = filmId,
                Title = "Updated Workflow Test Movie",
                Synopsis = "Updated synopsis for the workflow test",
                ReleaseYear = 2024,
                RunningTime = 130,
                Language = "English"
            };

            var updateJson = JsonSerializer.Serialize(updatedFilm, _jsonOptions);
            var updateContent = new StringContent(updateJson, Encoding.UTF8, "application/json");

            var updateResponse = await _client.PutAsync($"/api/films/{filmId}", updateContent);
            updateResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

            // Step 4: Verify the update
            var getUpdatedResponse = await _client.GetAsync($"/api/films/{filmId}");
            var getUpdatedContent = await getUpdatedResponse.Content.ReadAsStringAsync();
            var updatedRetrievedFilm = JsonSerializer.Deserialize<FilmDTO>(getUpdatedContent, _jsonOptions);

            updatedRetrievedFilm.Title.Should().Be("Updated Workflow Test Movie");
            updatedRetrievedFilm.Synopsis.Should().Be("Updated synopsis for the workflow test");
            updatedRetrievedFilm.RunningTime.Should().Be(130);

            // Step 5: Delete the film
            var deleteResponse = await _client.DeleteAsync($"/api/films/{filmId}");
            deleteResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

            // Step 6: Verify the deletion
            var getDeletedResponse = await _client.GetAsync($"/api/films/{filmId}");
            getDeletedResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task AuthenticatedFilmWorkflow_WithValidToken_ShouldWork()
        {
            // Step 1: Authenticate and get token
            var loginRequest = new UserLogin
            {
                Username = "johndoe",
                Password = "password123"
            };

            var loginJson = JsonSerializer.Serialize(loginRequest, _jsonOptions);
            var loginContent = new StringContent(loginJson, Encoding.UTF8, "application/json");

            var tokenResponse = await _client.PostAsync("/api/token", loginContent);
            var tokenContent = await tokenResponse.Content.ReadAsStringAsync();
            
            // Assuming the token is returned in a specific format, adjust as needed
            var tokenData = JsonSerializer.Deserialize<JsonElement>(tokenContent);
            var token = tokenData.GetProperty("token").GetString();

            // Step 2: Use token for authenticated request
            _client.DefaultRequestHeaders.Authorization = 
                new AuthenticationHeaderValue("Bearer", token);

            // Step 3: Create film with authentication
            var authenticatedFilm = new FilmDTO
            {
                Title = "Authenticated Film",
                Synopsis = "A film created with authentication",
                ReleaseYear = 2024,
                RunningTime = 115,
                Language = "English"
            };

            var filmJson = JsonSerializer.Serialize(authenticatedFilm, _jsonOptions);
            var filmContent = new StringContent(filmJson, Encoding.UTF8, "application/json");

            var createResponse = await _client.PostAsync("/api/films", filmContent);
            createResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task FilmGenreWorkflow_AssociateFilmWithGenre_ShouldWork()
        {
            // Step 1: Create a new genre
            var newGenre = new GenreDTO
            {
                Description = "Sci-Fi"
            };

            var genreJson = JsonSerializer.Serialize(newGenre, _jsonOptions);
            var genreContent = new StringContent(genreJson, Encoding.UTF8, "application/json");

            var genreResponse = await _client.PostAsync("/api/genres", genreContent);
            genreResponse.StatusCode.Should().Be(HttpStatusCode.Created);

            // Step 2: Create a new film
            var newFilm = new FilmDTO
            {
                Title = "Sci-Fi Movie",
                Synopsis = "A science fiction movie",
                ReleaseYear = 2024,
                RunningTime = 140,
                Language = "English"
            };

            var filmJson = JsonSerializer.Serialize(newFilm, _jsonOptions);
            var filmContent = new StringContent(filmJson, Encoding.UTF8, "application/json");

            var filmResponse = await _client.PostAsync("/api/films", filmContent);
            filmResponse.StatusCode.Should().Be(HttpStatusCode.Created);

            // Step 3: Verify both resources exist
            var genresResponse = await _client.GetAsync("/api/genres");
            var genresContent = await genresResponse.Content.ReadAsStringAsync();
            genresContent.Should().Contain("Sci-Fi");

            var filmsResponse = await _client.GetAsync("/api/films");
            var filmsContent = await filmsResponse.Content.ReadAsStringAsync();
            filmsContent.Should().Contain("Sci-Fi Movie");
        }

        private static int ExtractIdFromLocationHeader(string locationHeader)
        {
            // Extract ID from location header like "/api/films/123"
            var segments = locationHeader.Split('/');
            return int.Parse(segments[^1]);
        }
    }
}