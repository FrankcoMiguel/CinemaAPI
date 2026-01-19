using CinemaAPI.Api;
using CinemaAPI.Core.Entities;
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
    public class TokenControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Program> _factory;
        private readonly JsonSerializerOptions _jsonOptions;

        public TokenControllerIntegrationTests(CustomWebApplicationFactory<Program> factory)
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
        public async Task GetToken_ValidCredentials_ReturnsToken()
        {
            // Arrange
            var loginRequest = new UserLogin
            {
                Username = "johndoe",
                Password = "password123"
            };

            var json = JsonSerializer.Serialize(loginRequest, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/token", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            responseContent.Should().NotBeEmpty();
            responseContent.Should().Contain("token");
        }

        [Fact]
        public async Task GetToken_InvalidCredentials_ReturnsNotFound()
        {
            // Arrange
            var loginRequest = new UserLogin
            {
                Username = "invaliduser",
                Password = "wrongpassword"
            };

            var json = JsonSerializer.Serialize(loginRequest, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/token", content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetToken_EmptyCredentials_ReturnsBadRequest()
        {
            // Arrange
            var loginRequest = new UserLogin();

            var json = JsonSerializer.Serialize(loginRequest, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/token", content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GetToken_AdminCredentials_ReturnsTokenWithAdminClaims()
        {
            // Arrange
            var loginRequest = new UserLogin
            {
                Username = "admin",
                Password = "adminpassword"
            };

            var json = JsonSerializer.Serialize(loginRequest, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/token", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            responseContent.Should().NotBeEmpty();
            responseContent.Should().Contain("token");
        }
    }
}