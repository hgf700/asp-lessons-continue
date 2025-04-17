using AutoMapper;
using aspapp.Data.Models;
using aspapp.Data.Models.VM;

namespace aspapp.Data.Models.Mapper
{
    public class TripMapper : Profile
    {
        public TripMapper()
        {
            CreateMap<Trip, TripViewModel>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))

                .ForMember(dest => dest.GuideId, opt => opt.MapFrom(src => src.GuideId))
                .ForMember(dest => dest.Guides, opt => opt.MapFrom(src => src.Guide))

                .ForMember(dest => dest.TravelerId, opt => opt.MapFrom(src => src.TravelerId))
                .ForMember(dest => dest.Travelers, opt => opt.MapFrom(src => src.Travelers))

                .ForMember(dest => dest.Travelers, opt => opt.MapFrom(src => src.Travelers));

            CreateMap<TripViewModel, Trip>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))

                .ForMember(dest => dest.GuideId, opt => opt.MapFrom(src => src.GuideId))
                .ForMember(dest => dest.TravelerId, opt => opt.MapFrom(src => src.TravelerId))

                .ForMember(dest => dest.Guide, opt => opt.MapFrom(src => src.Guides))
                .ForMember(dest => dest.Travelers, opt => opt.MapFrom(src => src.Travelers));
        }
    }
}
