using AutoMapper;
using MovieStore.WebApi.Models.DTOs;
using MovieStore.WebApi.Models.Entities;

namespace MovieStore.WebApi.Mappings
{
    public class ObjectMapperProfile: Profile
    {
        public ObjectMapperProfile()
        {
            CreateMap<Actors, ActorViewModel>()
               .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Name + " " + src.Surname));

            CreateMap<Movies, MovieActorModel>()
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<MovieActorCreateModel, Movies>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<ActorCreateModel, Actors>();
           
        }
    }
}
