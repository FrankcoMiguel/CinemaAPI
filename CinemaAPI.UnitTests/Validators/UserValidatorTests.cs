using CinemaAPI.Core.DTOs;
using CinemaAPI.Infrastructure.Validators;
using FluentValidation.TestHelper;
using Xunit;

namespace CinemaAPI.UnitTests.Validators
{
    public class UserValidatorTests
    {
        private readonly UserValidator _validator;

        public UserValidatorTests()
        {
            _validator = new UserValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Firstname_Is_Null()
        {
            var model = new UserDTO { Firstname = null };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(user => user.Firstname);
        }

        [Fact]
        public void Should_Have_Error_When_Firstname_Is_Too_Short()
        {
            var model = new UserDTO { Firstname = "A" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(user => user.Firstname);
        }

        [Fact]
        public void Should_Have_Error_When_Firstname_Is_Too_Long()
        {
            var model = new UserDTO { Firstname = new string('A', 51) };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(user => user.Firstname);
        }

        [Fact]
        public void Should_Have_Error_When_Lastname_Is_Null()
        {
            var model = new UserDTO { Lastname = null };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(user => user.Lastname);
        }

        [Fact]
        public void Should_Have_Error_When_Username_Is_Null()
        {
            var model = new UserDTO { Username = null };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(user => user.Username);
        }

        [Fact]
        public void Should_Have_Error_When_Password_Is_Null()
        {
            var model = new UserDTO { Password = null };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(user => user.Password);
        }

        [Fact]
        public void Should_Have_Error_When_Password_Is_Too_Long()
        {
            var model = new UserDTO { Password = new string('A', 226) };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(user => user.Password);
        }

        [Fact]
        public void Should_Have_Error_When_Role_Is_Null()
        {
            var model = new UserDTO { Role = null };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(user => user.Role);
        }

        [Fact]
        public void Should_Not_Have_Any_Errors_When_User_Is_Valid()
        {
            var model = new UserDTO
            {
                Firstname = "John",
                Lastname = "Doe",
                Username = "johndoe",
                Password = "password123",
                Role = CinemaAPI.Core.Enumerations.RoleType.Consumer
            };

            var result = _validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}