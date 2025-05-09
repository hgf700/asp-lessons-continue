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
            .ForMember(dest => dest.GuideId, opt => opt.MapFrom(src => src.GuideId))
            .ForMember(dest => dest.Guide, opt => opt.MapFrom(src => src.Guide))
            .ForMember(dest => dest.Travelers, opt => opt.MapFrom(src => src.Travelers));

        // TripViewModel → Trip
        CreateMap<TripViewModel, Trip>()
            .ForMember(dest => dest.TripId, opt => opt.MapFrom(src => src.TripId))
            .ForMember(dest => dest.Destination, opt => opt.MapFrom(src => src.Destination))
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
            .ForMember(dest => dest.GuideId, opt => opt.MapFrom(src => src.GuideId))
            .ForMember(dest => dest.Guide, opt => opt.Ignore()) // GuideId should suffice
            .ForMember(dest => dest.Travelers, opt => opt.MapFrom(src => src.Travelers));

        // Traveler → TravelerViewModel
        CreateMap<Traveler, TravelerViewModel>()
            .ForMember(dest => dest.TravelerId, opt => opt.MapFrom(src => src.TravelerId))
            .ForMember(dest => dest.Firstname, opt => opt.MapFrom(src => src.Firstname))
            .ForMember(dest => dest.Lastname, opt => opt.MapFrom(src => src.Lastname))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate));

        // TravelerViewModel → Traveler
        CreateMap<TravelerViewModel, Traveler>()
            .ForMember(dest => dest.TravelerId, opt => opt.MapFrom(src => src.TravelerId))
            .ForMember(dest => dest.Firstname, opt => opt.MapFrom(src => src.Firstname))
            .ForMember(dest => dest.Lastname, opt => opt.MapFrom(src => src.Lastname))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate));

        // Guide → GuideViewModel
        CreateMap<Guide, GuideViewModel>()
            .ForMember(dest => dest.GuideId, opt => opt.MapFrom(src => src.GuideId))
            .ForMember(dest => dest.Firstname, opt => opt.MapFrom(src => src.Firstname))
            .ForMember(dest => dest.Lastname, opt => opt.MapFrom(src => src.Lastname))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Trips, opt => opt.Ignore());

        // GuideViewModel → Guide
        CreateMap<GuideViewModel, Guide>()
            .ForMember(dest => dest.GuideId, opt => opt.MapFrom(src => src.GuideId))
            .ForMember(dest => dest.Firstname, opt => opt.MapFrom(src => src.Firstname))
            .ForMember(dest => dest.Lastname, opt => opt.MapFrom(src => src.Lastname))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Trips, opt => opt.Ignore());
    }
}
