using CinemaAPI.Core.Entities;
using CinemaAPI.Core.Enumerations;
using CinemaAPI.Infrastructure.Data;
using CinemaAPI.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaAPI.Infrastructure.Data
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<CinemaAPIContext>();
            var passwordService = scope.ServiceProvider.GetRequiredService<IPasswordService>();

            // Ensure database is created
            await context.Database.EnsureCreatedAsync();

            // Check if data already exists
            if (context.User.Any())
            {
                return; // DB has been seeded
            }

            // Seed Ratings
            var ratings = new[]
            {
                new Rating { Name = "G", Description = "General Audiences" },
                new Rating { Name = "PG", Description = "Parental Guidance Suggested" },
                new Rating { Name = "PG-13", Description = "Parents Strongly Cautioned" },
                new Rating { Name = "R", Description = "Restricted" },
                new Rating { Name = "NC-17", Description = "Adults Only" }
            };

            await context.Rating.AddRangeAsync(ratings);
            await context.SaveChangesAsync();

            // Seed Genres
            var genres = new[]
            {
                new Genre { Name = "Action", Description = "Action movies" },
                new Genre { Name = "Comedy", Description = "Comedy movies" },
                new Genre { Name = "Drama", Description = "Drama movies" },
                new Genre { Name = "Horror", Description = "Horror movies" },
                new Genre { Name = "Romance", Description = "Romance movies" },
                new Genre { Name = "Sci-Fi", Description = "Science Fiction movies" },
                new Genre { Name = "Thriller", Description = "Thriller movies" },
                new Genre { Name = "Fantasy", Description = "Fantasy movies" }
            };

            await context.Genre.AddRangeAsync(genres);
            await context.SaveChangesAsync();

            // Seed Occupations
            var occupations = new[]
            {
                new Occupation { Name = "Actor", Description = "Film actor/actress" },
                new Occupation { Name = "Director", Description = "Film director" },
                new Occupation { Name = "Producer", Description = "Film producer" },
                new Occupation { Name = "Writer", Description = "Screenwriter" },
                new Occupation { Name = "Cinematographer", Description = "Director of photography" },
                new Occupation { Name = "Editor", Description = "Film editor" },
                new Occupation { Name = "Composer", Description = "Music composer" }
            };

            await context.Occupation.AddRangeAsync(occupations);
            await context.SaveChangesAsync();

            // Seed Users
            var users = new[]
            {
                new User 
                { 
                    Firstname = "Admin", 
                    Lastname = "User", 
                    Username = "admin", 
                    Password = passwordService.Hash("admin123"),
                    Role = RoleType.Admin 
                },
                new User 
                { 
                    Firstname = "John", 
                    Lastname = "Doe", 
                    Username = "john.doe", 
                    Password = passwordService.Hash("password123"),
                    Role = RoleType.Consumer 
                }
            };

            await context.User.AddRangeAsync(users);
            await context.SaveChangesAsync();

            // Seed Persons
            var persons = new[]
            {
                new Person 
                { 
                    Firstname = "Leonardo", 
                    Lastname = "DiCaprio", 
                    Age = 49, 
                    Description = "American actor and producer",
                    IsActive = "Y"
                },
                new Person 
                { 
                    Firstname = "Christopher", 
                    Lastname = "Nolan", 
                    Age = 53, 
                    Description = "British-American film director, producer, and screenwriter",
                    IsActive = "Y"
                },
                new Person 
                { 
                    Firstname = "Margot", 
                    Lastname = "Robbie", 
                    Age = 33, 
                    Description = "Australian actress and producer",
                    IsActive = "Y"
                }
            };

            await context.Person.AddRangeAsync(persons);
            await context.SaveChangesAsync();

            // Seed Films
            var pgRating = ratings.First(r => r.Name == "PG-13");
            var films = new[]
            {
                new Film
                {
                    Title = "Inception",
                    Synopsis = "A thief who steals corporate secrets through the use of dream-sharing technology is given the inverse task of planting an idea into the mind of a C.E.O.",
                    ReleaseYear = 2010,
                    RunningTime = 148,
                    Language = "English",
                    RatingId = pgRating.Id,
                    Score = 8.8m,
                    Budget = 160000000,
                    BoxOffice = 836800000,
                    PictureReference = "inception.jpg"
                },
                new Film
                {
                    Title = "The Dark Knight",
                    Synopsis = "When the menace known as the Joker wreaks havoc and chaos on the people of Gotham, Batman must accept one of the greatest psychological and physical tests of his ability to fight injustice.",
                    ReleaseYear = 2008,
                    RunningTime = 152,
                    Language = "English",
                    RatingId = pgRating.Id,
                    Score = 9.0m,
                    Budget = 185000000,
                    BoxOffice = 1004558444,
                    PictureReference = "dark_knight.jpg"
                }
            };

            await context.Film.AddRangeAsync(films);
            await context.SaveChangesAsync();
        }
    }
}