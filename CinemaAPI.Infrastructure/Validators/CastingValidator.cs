using CinemaAPI.Core.DTOs;
using FluentValidation;

namespace CinemaAPI.Infrastructure.Validators
{
    public class CastingValidator : AbstractValidator<CastingDTO>
    {
        public CastingValidator()
        {
            RuleFor(casting => casting.PersonId)
                .NotNull()
                .WithMessage("PersonId is required")
                .GreaterThan(0)
                .WithMessage("PersonId must be greater than 0");

            RuleFor(casting => casting.FilmId)
                .NotNull()
                .WithMessage("FilmId is required")
                .GreaterThan(0)
                .WithMessage("FilmId must be greater than 0");

            RuleFor(casting => casting.OccupationId)
                .NotNull()
                .WithMessage("OccupationId is required")
                .GreaterThan(0)
                .WithMessage("OccupationId must be greater than 0");
        }
    }
}