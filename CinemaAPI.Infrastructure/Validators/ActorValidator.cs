using CinemaAPI.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaAPI.Infrastructure.Validators
{
    public class ActorValidator : AbstractValidator<ActorDTO>
    {
        public ActorValidator()
        {
            RuleFor(actor => actor.Firstname)
                 .NotNull()
                 .Length(2, 20);

            RuleFor(actor => actor.Age)
                .NotNull()
                .GreaterThan(0);
        }

    }
}
