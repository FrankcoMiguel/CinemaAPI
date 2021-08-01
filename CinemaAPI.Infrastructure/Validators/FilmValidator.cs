using CinemaAPI.Core.DTOs;
using FluentValidation;
using System;

namespace CinemaAPI.Infrastructure.Validators
{
    public class FilmValidator : AbstractValidator<FilmDTO>
    {
        public FilmValidator()
        {
            RuleFor(film => film.Title)
                .NotNull()
                .Length(2, 200);

            RuleFor(film => film.Synopsis)
                .NotNull()
                .Length(5, 3999);

            RuleFor(film => film.ReleaseYear)
                .NotNull()
                .GreaterThan(1000)
                .LessThanOrEqualTo(DateTime.Now.Year);

            RuleFor(film => film.RunningTime)
                .NotNull()
                .GreaterThan(0);

            RuleFor(film => film.Language)
                .NotNull();

            RuleFor(film => film.Score)
                .NotNull()
                .GreaterThan(0)
                .LessThanOrEqualTo(10);

        }
    }
}
