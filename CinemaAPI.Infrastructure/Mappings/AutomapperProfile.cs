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
            CreateMap<Actor, ActorDTO>();
            CreateMap<ActorDTO, Actor>();
        }
    }
}
