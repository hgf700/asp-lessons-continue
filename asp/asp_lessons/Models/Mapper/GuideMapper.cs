using AutoMapper;
using aspapp.Models;
using aspapp.Models.VM;

namespace aspapp.Profiles
{
    public class GuideProfile : Profile
    {
        public GuideProfile()
        {
            // Mapowanie między Guide i GuideViewModel
            CreateMap<Guide, GuideViewModel>()
                .ForMember(dest => dest.GuideId, opt => opt.MapFrom(src => src.GuideId))
                .ForMember(dest => dest.Firstname, opt => opt.MapFrom(src => src.Firstname))
                .ForMember(dest => dest.Lastname, opt => opt.MapFrom(src => src.Lastname))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Trips, opt => opt.MapFrom(src => src.Trips)); // Mapowanie do TripViewModel

            // Mapowanie między GuideViewModel a Guide
            CreateMap<GuideViewModel, Guide>()
                .ForMember(dest => dest.GuideId, opt => opt.MapFrom(src => src.GuideId))
                .ForMember(dest => dest.Firstname, opt => opt.MapFrom(src => src.Firstname))
                .ForMember(dest => dest.Lastname, opt => opt.MapFrom(src => src.Lastname))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Trips, opt => opt.MapFrom(src => src.Trips)); // Mapowanie do Trip
        }
    }
}
