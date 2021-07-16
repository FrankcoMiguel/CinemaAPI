using AutoMapper;
using CinemaAPI.Core.DTOs;
using CinemaAPI.Core.Entities;

namespace CinemaAPI.Infrastructure.Mappings
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Film, FilmDTO>();
            CreateMap<FilmDTO, Film>();

            CreateMap<Genre, GenreDTO>();
            CreateMap<GenreDTO, Genre>();

            CreateMap<Occupation, OccupationDTO>();
            CreateMap<OccupationDTO, Occupation>();

            CreateMap<Person, PersonDTO>();
            CreateMap<PersonDTO, Person>();

            CreateMap<Rating, RatingDTO>();
            CreateMap<RatingDTO, Rating>();


            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();

        }
    }
}
