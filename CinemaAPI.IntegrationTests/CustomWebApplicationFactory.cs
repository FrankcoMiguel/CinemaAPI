using CinemaAPI.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace CinemaAPI.IntegrationTests
{
    public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");

            builder.ConfigureServices((hostBuilderContext, services) =>
            {
                // Remove all existing DbContext and related registrations
                var toRemove = services
                    .Where(descriptor => 
                        descriptor.ServiceType == typeof(CinemaAPIContext) ||
                        descriptor.ServiceType == typeof(DbContextOptions<CinemaAPIContext>) ||
                        descriptor.ServiceType == typeof(DbContextOptions) ||
                        (descriptor.ImplementationType?.IsAssignableFrom(typeof(CinemaAPIContext)) == true) ||
                        (descriptor.ServiceType.IsGenericType && descriptor.ServiceType.GetGenericTypeDefinition() == typeof(DbContextOptions<>))
                    )
                    .ToArray();

                foreach (var descriptor in toRemove)
                {
                    services.Remove(descriptor);
                }

                // Add InMemory database configuration
                services.AddDbContext<CinemaAPIContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryTestDatabase")
                           .EnableSensitiveDataLogging()
                           .EnableServiceProviderCaching(false)
                           .EnableDetailedErrors();
                });
            });
        }

        public void SeedDatabase()
        {
            using var scope = Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<CinemaAPIContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<CustomWebApplicationFactory<TProgram>>>();

            try
            {
                // Clear any existing data
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                SeedTestData(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred seeding the database with test messages. Error: {Message}", ex.Message);
                throw;
            }
        }

        private static void SeedTestData(CinemaAPIContext context)
        {
            if (context.Film.Any())
            {
                return;
            }

            context.Film.AddRange(
                new Core.Entities.Film
                {
                    Id = 1,
                    Title = "Test Movie 1",
                    Synopsis = "Test synopsis for movie 1",
                    ReleaseYear = 2020,
                    RunningTime = 120,
                    Language = "English",
                    PictureReference = "test1.jpg"
                },
                new Core.Entities.Film
                {
                    Id = 2,
                    Title = "Test Movie 2",
                    Synopsis = "Test synopsis for movie 2",
                    ReleaseYear = 2021,
                    RunningTime = 135,
                    Language = "English",
                    PictureReference = "test2.jpg"
                }
            );

            context.Genre.AddRange(
                new Core.Entities.Genre
                {
                    Id = 1,
                    Description = "Action"
                },
                new Core.Entities.Genre
                {
                    Id = 2,
                    Description = "Comedy"
                }
            );

            context.User.AddRange(
                new Core.Entities.User
                {
                    Id = 1,
                    Firstname = "John",
                    Lastname = "Doe",
                    Username = "johndoe",
                    Password = "hashedpassword123",
                    Role = Core.Enumerations.RoleType.Consumer
                },
                new Core.Entities.User
                {
                    Id = 2,
                    Firstname = "Admin",
                    Lastname = "User",
                    Username = "admin",
                    Password = "adminpass123",
                    Role = Core.Enumerations.RoleType.Admin
                }
            );

            context.SaveChanges();
        }
    }
}