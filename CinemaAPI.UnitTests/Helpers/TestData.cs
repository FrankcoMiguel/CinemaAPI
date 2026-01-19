using CinemaAPI.Core.Entities;
using CinemaAPI.Core.Enumerations;
using System.Collections.Generic;

namespace CinemaAPI.UnitTests.Helpers
{
    public static class TestData
    {
        public static List<Genre> GetGenres()
        {
            return new List<Genre>
            {
                new Genre { Id = 1, Name = "Action", Description = "Action movies" },
                new Genre { Id = 2, Name = "Comedy", Description = "Comedy movies" },
                new Genre { Id = 3, Name = "Drama", Description = "Drama movies" }
            };
        }

        public static List<Rating> GetRatings()
        {
            return new List<Rating>
            {
                new Rating { Id = 1, Name = "G", Description = "General Audiences" },
                new Rating { Id = 2, Name = "PG", Description = "Parental Guidance" },
                new Rating { Id = 3, Name = "PG-13", Description = "Parents Strongly Cautioned" }
            };
        }

        public static List<Person> GetPersons()
        {
            return new List<Person>
            {
                new Person { Id = 1, Firstname = "John", Lastname = "Doe", Age = 30, Description = "Actor", IsActive = "Y" },
                new Person { Id = 2, Firstname = "Jane", Lastname = "Smith", Age = 25, Description = "Director", IsActive = "Y" },
                new Person { Id = 3, Firstname = "Bob", Lastname = "Johnson", Age = 40, Description = "Producer", IsActive = "N" }
            };
        }

        public static List<Occupation> GetOccupations()
        {
            return new List<Occupation>
            {
                new Occupation { Id = 1, Name = "Actor", Description = "Film actor" },
                new Occupation { Id = 2, Name = "Director", Description = "Film director" },
                new Occupation { Id = 3, Name = "Producer", Description = "Film producer" }
            };
        }

        public static List<Film> GetFilms()
        {
            return new List<Film>
            {
                new Film 
                { 
                    Id = 1, 
                    Title = "Test Movie 1", 
                    Synopsis = "A test movie", 
                    ReleaseYear = 2022, 
                    RunningTime = 120, 
                    Language = "English",
                    RatingId = 1,
                    Score = 8.5m
                },
                new Film 
                { 
                    Id = 2, 
                    Title = "Test Movie 2", 
                    Synopsis = "Another test movie", 
                    ReleaseYear = 2023, 
                    RunningTime = 90, 
                    Language = "Spanish",
                    RatingId = 2,
                    Score = 7.8m
                }
            };
        }

        public static List<User> GetUsers()
        {
            return new List<User>
            {
                new User 
                { 
                    Id = 1, 
                    Firstname = "Admin", 
                    Lastname = "User", 
                    Username = "admin", 
                    Password = "hashedpassword",
                    Role = RoleType.Admin
                },
                new User 
                { 
                    Id = 2, 
                    Firstname = "Test", 
                    Lastname = "User", 
                    Username = "testuser", 
                    Password = "hashedpassword",
                    Role = RoleType.Consumer
                }
            };
        }
    }
}