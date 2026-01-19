using CinemaAPI.Core.DTOs;
using FluentValidation;

namespace CinemaAPI.Infrastructure.Validators
{
    public class GenreValidator : AbstractValidator<GenreDTO>
    {
        public GenreValidator()
        {
            RuleFor(genre => genre.Name)
                .NotNull()
                .WithMessage("Genre name is required")
                .Length(2, 50)
                .WithMessage("Genre name must be between 2 and 50 characters");

            RuleFor(genre => genre.Description)
                .Length(5, 500)
                .When(genre => !string.IsNullOrEmpty(genre.Description))
                .WithMessage("Genre description must be between 5 and 500 characters");
        }
    }
}