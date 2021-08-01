using CinemaAPI.Core.DTOs;
using FluentValidation;
using System;

namespace CinemaAPI.Infrastructure.Validators
{
    public class UserValidator : AbstractValidator<UserDTO>
    {
        public UserValidator()
        {
            RuleFor(user => user.Firstname)
                .NotNull()
                .Length(2, 50);

            RuleFor(user => user.Lastname)
                .NotNull()
                .Length(2, 50);

            RuleFor(user => user.Username)
                .NotNull()
                .Length(2, 50);

            RuleFor(user => user.Password)
                .NotNull()
                .Length(2, 225);

            RuleFor(user => user.Role)
                .NotNull()
                .IsInEnum()
                .WithMessage("This role is undefined");

        }
    }
}
