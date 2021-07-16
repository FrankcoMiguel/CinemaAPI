using CinemaAPI.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaAPI.Infrastructure.Validators
{
    public class AgeRatingValidator : AbstractValidator<AgeRatingDTO>
    {
        public AgeRatingValidator()
        {
            RuleFor(ageRating => ageRating.Code)
                 .NotNull()
                 .Length(1, 10);

            RuleFor(ageRating => ageRating.Name)
                .NotNull()
                .Length(4, 150);

            RuleFor(ageRating => ageRating.Description)
                .NotNull()
                .Length(10, 2000);
        }

    }
}
