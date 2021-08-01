using CinemaAPI.Core.DTOs;
using FluentValidation;
using System;

namespace CinemaAPI.Infrastructure.Validators
{
    public class PersonValidator : AbstractValidator<PersonDTO>
    {
        public PersonValidator()
        {
            RuleFor(person => person.Firstname)
                .NotNull()
                .Length(2, 50);

            RuleFor(person => person.Lastname)
                .NotNull()
                .Length(2, 50);

            RuleFor(person => person.Description)
                .Length(2, 3999);

            RuleFor(person => person.Age)
                .GreaterThan(0)
                .NotNull();

            RuleFor(person => person.IsActive)
                .NotNull()
                .Length(1, 1)
                .WithMessage("The active value must be 'Y' for yes or 'N' for no");

        }
    }
}
