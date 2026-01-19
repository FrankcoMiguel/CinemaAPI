using CinemaAPI.Core.DTOs;
using FluentValidation;

namespace CinemaAPI.Infrastructure.Validators
{
    public class RatingValidator : AbstractValidator<RatingDTO>
    {
        public RatingValidator()
        {
            RuleFor(rating => rating.Name)
                .NotNull()
                .WithMessage("Rating name is required")
                .Length(1, 20)
                .WithMessage("Rating name must be between 1 and 20 characters");

            RuleFor(rating => rating.Description)
                .Length(5, 500)
                .When(rating => !string.IsNullOrEmpty(rating.Description))
                .WithMessage("Rating description must be between 5 and 500 characters");
        }
    }
}