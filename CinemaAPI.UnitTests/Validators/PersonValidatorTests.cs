using CinemaAPI.Core.DTOs;
using CinemaAPI.Infrastructure.Validators;
using FluentValidation.TestHelper;
using Xunit;

namespace CinemaAPI.UnitTests.Validators
{
    public class PersonValidatorTests
    {
        private readonly PersonValidator _validator;

        public PersonValidatorTests()
        {
            _validator = new PersonValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Firstname_Is_Null()
        {
            var model = new PersonDTO { Firstname = null };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(person => person.Firstname);
        }

        [Fact]
        public void Should_Have_Error_When_Firstname_Is_Too_Short()
        {
            var model = new PersonDTO { Firstname = "A" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(person => person.Firstname);
        }

        [Fact]
        public void Should_Have_Error_When_Lastname_Is_Null()
        {
            var model = new PersonDTO { Lastname = null };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(person => person.Lastname);
        }

        [Fact]
        public void Should_Have_Error_When_Age_Is_Zero_Or_Negative()
        {
            var model = new PersonDTO { Age = 0 };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(person => person.Age);
        }

        [Fact]
        public void Should_Have_Error_When_IsActive_Is_Null()
        {
            var model = new PersonDTO { IsActive = null };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(person => person.IsActive);
        }

        [Fact]
        public void Should_Have_Error_When_IsActive_Is_Invalid_Length()
        {
            var model = new PersonDTO { IsActive = "YES" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(person => person.IsActive);
        }

        [Fact]
        public void Should_Have_Error_When_Description_Is_Too_Short()
        {
            var model = new PersonDTO { Description = "A" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(person => person.Description);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Description_Is_Null()
        {
            var model = new PersonDTO { Description = null };
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(person => person.Description);
        }

        [Fact]
        public void Should_Not_Have_Any_Errors_When_Person_Is_Valid()
        {
            var model = new PersonDTO
            {
                Firstname = "John",
                Lastname = "Doe",
                Description = "A famous actor",
                Age = 30,
                IsActive = "Y"
            };

            var result = _validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}