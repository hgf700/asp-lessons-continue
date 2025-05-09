using aspapp.Models.VM;
using aspapp.Models;
using AutoMapper;

public class TripProfile : Profile
{
    public TripProfile()
    {
        // Trip → TripViewModel
        CreateMap<Trip, TripViewModel>()
            .ForMember(dest => dest.TripId, opt => opt.MapFrom(src => src.TripId))
            .ForMember(dest => dest.Destination, opt => opt.MapFrom(src => src.Destination))
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
            .ForMember(dest => dest.GuideId, opt => opt.MapFrom(src => src.GuideId)) // ← ADD THIS
            .ForMember(dest => dest.Travelers, opt => opt.MapFrom(src => src.Travelers))
            .ForMember(dest => dest.Guides, opt => opt.MapFrom(src => src.Guides));

        // TripViewModel → Trip
        CreateMap<TripViewModel, Trip>()
            .ForMember(dest => dest.TripId, opt => opt.MapFrom(src => src.TripId))
            .ForMember(dest => dest.Destination, opt => opt.MapFrom(src => src.Destination))
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
            .ForMember(dest => dest.GuideId, opt => opt.MapFrom(src => src.GuideId)) // ← ADD THIS
            .ForMember(dest => dest.Travelers, opt => opt.MapFrom(src => src.Travelers))
            .ForMember(dest => dest.Guides, opt => opt.MapFrom(src => src.Guides));
    }
}
