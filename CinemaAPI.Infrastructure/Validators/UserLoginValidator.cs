using CinemaAPI.Core.Entities;
using FluentValidation;

namespace CinemaAPI.Infrastructure.Validators
{
    public class UserLoginValidator : AbstractValidator<UserLogin>
    {
        public UserLoginValidator()
        {
            RuleFor(login => login.Username)
                .NotNull()
                .WithMessage("Username is required")
                .Length(2, 50)
                .WithMessage("Username must be between 2 and 50 characters");

            RuleFor(login => login.Password)
                .NotNull()
                .WithMessage("Password is required")
                .MinimumLength(6)
                .WithMessage("Password must be at least 6 characters long");
        }
    }
}