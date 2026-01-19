using CinemaAPI.Core.Entities;
using System.Collections.Generic;
using System.Linq;

namespace CinemaAPI.UnitTests.TestHelpers
{
    public static class TestData
    {
        public static Film GetFilm(int id)
        {
            return new Film
            {
                Id = id,
                Title = $"Test Film {id}",
                Synopsis = $"Test Synopsis {id}",
                ReleaseYear = 2020 + id,
                RunningTime = 120 + id,
                Language = "English",
                PictureReference = $"image{id}.jpg"
            };
        }

        public static IEnumerable<Film> GetFilms(int count = 3)
        {
            return Enumerable.Range(1, count).Select(i => GetFilm(i));
        }

        public static Genre GetGenre(int id)
        {
            return new Genre
            {
                Id = id,
                Description = $"Test Genre {id}"
            };
        }

        public static IEnumerable<Genre> GetGenres(int count = 3)
        {
            return Enumerable.Range(1, count).Select(i => GetGenre(i));
        }

        public static User GetUser(int id)
        {
            return new User
            {
                Id = id,
                Firstname = $"FirstName{id}",
                Lastname = $"LastName{id}",
                Username = $"user{id}",
                Password = "password123",
                Role = Core.Enumerations.RoleType.Consumer
            };
        }

        public static IEnumerable<User> GetUsers(int count = 3)
        {
            return Enumerable.Range(1, count).Select(i => GetUser(i));
        }

        public static Person GetPerson(int id)
        {
            return new Person
            {
                Id = id,
                Firstname = $"FirstName{id}",
                Lastname = $"LastName{id}",
                Description = $"Actor description {id}",
                Age = 30 + id,
                IsActive = "Y",
                PictureReference = $"person{id}.jpg"
            };
        }

        public static IEnumerable<Person> GetPersons(int count = 3)
        {
            return Enumerable.Range(1, count).Select(i => GetPerson(i));
        }

        public static Rating GetRating(int id)
        {
            return new Rating
            {
                Id = id,
                Code = $"R{id}",
                Name = $"Rating {id}",
                Description = $"Test Rating Description {id}"
            };
        }

        public static IEnumerable<Rating> GetRatings(int count = 3)
        {
            return Enumerable.Range(1, count).Select(i => GetRating(i));
        }

        public static Occupation GetOccupation(int id)
        {
            return new Occupation
            {
                Id = id,
                Name = $"Test Occupation {id}",
                Description = $"Test Occupation Description {id}"
            };
        }

        public static IEnumerable<Occupation> GetOccupations(int count = 3)
        {
            return Enumerable.Range(1, count).Select(i => GetOccupation(i));
        }
    }
}