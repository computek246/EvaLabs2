using AutoMapper;
using EvaLabs.Domain.Entities;
using EvaLabs.ViewModels;

namespace EvaLabs.Mapping.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Test, TestViewModel>().ReverseMap();
            CreateMap<City, CityViewModel>().ReverseMap();
            CreateMap<Area, AreaViewModel>()
                .ForMember(e => e.CityName, x => x.MapFrom(e => e.City.CityName))
                .ReverseMap();
            CreateMap<Branch, BranchViewModel>()
                .ForMember(e => e.AreaName, x => x.MapFrom(e => e.Area.AreaName))
                .ForMember(e => e.CityName, x => x.MapFrom(e => e.Area.City.CityName))
                .ReverseMap();
        }
    }
}
