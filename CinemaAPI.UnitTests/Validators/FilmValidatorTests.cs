using CinemaAPI.Core.DTOs;
using CinemaAPI.Infrastructure.Validators;
using FluentAssertions;
using FluentValidation.TestHelper;
using Xunit;

namespace CinemaAPI.UnitTests.Validators
{
    public class FilmValidatorTests
    {
        private readonly FilmValidator _validator;

        public FilmValidatorTests()
        {
            _validator = new FilmValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Title_Is_Null()
        {
            var model = new FilmDTO { Title = null };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(film => film.Title);
        }

        [Fact]
        public void Should_Have_Error_When_Title_Is_Too_Short()
        {
            var model = new FilmDTO { Title = "A" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(film => film.Title);
        }

        [Fact]
        public void Should_Have_Error_When_Title_Is_Too_Long()
        {
            var model = new FilmDTO { Title = new string('A', 201) };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(film => film.Title);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Title_Is_Valid()
        {
            var model = new FilmDTO { Title = "Valid Movie Title" };
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(film => film.Title);
        }

        [Fact]
        public void Should_Have_Error_When_Synopsis_Is_Null()
        {
            var model = new FilmDTO { Synopsis = null };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(film => film.Synopsis);
        }

        [Fact]
        public void Should_Have_Error_When_Synopsis_Is_Too_Short()
        {
            var model = new FilmDTO { Synopsis = "ABC" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(film => film.Synopsis);
        }

        [Fact]
        public void Should_Have_Error_When_ReleaseYear_Is_Too_Small()
        {
            var model = new FilmDTO { ReleaseYear = 999 };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(film => film.ReleaseYear);
        }

        [Fact]
        public void Should_Have_Error_When_ReleaseYear_Is_In_Future()
        {
            var model = new FilmDTO { ReleaseYear = System.DateTime.Now.Year + 1 };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(film => film.ReleaseYear);
        }

        [Fact]
        public void Should_Have_Error_When_RunningTime_Is_Zero_Or_Negative()
        {
            var model = new FilmDTO { RunningTime = 0 };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(film => film.RunningTime);
        }

        [Fact]
        public void Should_Have_Error_When_Language_Is_Null()
        {
            var model = new FilmDTO { Language = null };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(film => film.Language);
        }

        [Fact]
        public void Should_Have_Error_When_Score_Is_Too_High()
        {
            var model = new FilmDTO { Score = 11 };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(film => film.Score);
        }

        [Fact]
        public void Should_Have_Error_When_Score_Is_Zero_Or_Negative()
        {
            var model = new FilmDTO { Score = 0 };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(film => film.Score);
        }

        [Fact]
        public void Should_Not_Have_Any_Errors_When_Film_Is_Valid()
        {
            var model = new FilmDTO
            {
                Title = "Valid Movie Title",
                Synopsis = "This is a valid synopsis for a movie",
                ReleaseYear = 2022,
                RunningTime = 120,
                Language = "English",
                Score = 8.5m
            };

            var result = _validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}