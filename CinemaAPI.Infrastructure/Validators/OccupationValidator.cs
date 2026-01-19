using CinemaAPI.Core.DTOs;
using FluentValidation;

namespace CinemaAPI.Infrastructure.Validators
{
    public class OccupationValidator : AbstractValidator<OccupationDTO>
    {
        public OccupationValidator()
        {
            RuleFor(occupation => occupation.Name)
                .NotNull()
                .WithMessage("Occupation name is required")
                .Length(2, 50)
                .WithMessage("Occupation name must be between 2 and 50 characters");

            RuleFor(occupation => occupation.Description)
                .Length(5, 500)
                .When(occupation => !string.IsNullOrEmpty(occupation.Description))
                .WithMessage("Occupation description must be between 5 and 500 characters");
        }
    }
}