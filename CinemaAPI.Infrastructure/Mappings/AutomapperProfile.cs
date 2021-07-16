using AutoMapper;
using CinemaAPI.Core.DTOs;
using CinemaAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaAPI.Infrastructure.Mappings
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<AgeRating, AgeRatingDTO>();
            CreateMap<AgeRatingDTO, AgeRating>();

            CreateMap<Genre, GenreDTO>();
            CreateMap<GenreDTO, Genre>();

        }
    }
}
