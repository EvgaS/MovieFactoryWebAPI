using AutoMapper;
using MovieFactoryWebAPI.Models;
using MovieFactoryWebAPI.DTo;

namespace MovieFactoryWebAPI.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Actor, ActorDto>();
            CreateMap<MovieDto, Movie>();
            CreateMap<RoleDto, Role>();
            CreateMap<ActorDto, Actor>();
            CreateMap<Movie, MovieDto>();
            CreateMap<Role, RoleDto>();
            CreateMap<Role, RoleCSVDto>();
        }
    }
}
