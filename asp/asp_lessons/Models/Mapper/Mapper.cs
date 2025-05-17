using aspapp.Models.VM;
using aspapp.Models;
using AutoMapper;

public class TripProfile : Profile
{
    public TripProfile()
    {
        CreateMap<Trip, TripViewModel>()
                   // Nullable GuideId / TravelerId - zamiana int → int?
                   .ForMember(dest => dest.GuideId, opt => opt.MapFrom(src => (int?)src.GuideId))
                   .ForMember(dest => dest.TravelerId, opt => opt.MapFrom(src => (int?)src.TravelerId))
                   .ForMember(dest => dest.Guides, opt => opt.Ignore())     // Załadujesz osobno, np. z bazy
                   .ForMember(dest => dest.Travelers, opt => opt.Ignore());

        // TripViewModel → Trip
        CreateMap<TripViewModel, Trip>()
            // Nullable int? → int (wymaga null-check)
            .ForMember(dest => dest.GuideId, opt => opt.MapFrom(src => src.GuideId ?? 0))
            .ForMember(dest => dest.TravelerId, opt => opt.MapFrom(src => src.TravelerId ?? 0))
            .ForMember(dest => dest.Guide, opt => opt.Ignore())       // Załadujesz osobno jeśli potrzebne
            .ForMember(dest => dest.Traveler, opt => opt.Ignore());

        //Traveler → TravelerViewModel i odwrotnie
        CreateMap<Traveler, TravelerViewModel>().ReverseMap();

        // Guide → GuideViewModel i odwrotnie
        CreateMap<Guide, GuideViewModel>()
            .ForMember(dest => dest.Trips, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(dest => dest.Trips, opt => opt.Ignore());

        // Traveler → TravelerViewModel
        //CreateMap<Traveler, TravelerViewModel>()
        //    .ForMember(dest => dest.TravelerId, opt => opt.MapFrom(src => src.TravelerId))
        //    .ForMember(dest => dest.Firstname, opt => opt.MapFrom(src => src.Firstname))
        //    .ForMember(dest => dest.Lastname, opt => opt.MapFrom(src => src.Lastname))
        //    .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
        //    .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate));

        //// TravelerViewModel → Traveler
        //CreateMap<TravelerViewModel, Traveler>()
        //    .ForMember(dest => dest.TravelerId, opt => opt.MapFrom(src => src.TravelerId))
        //    .ForMember(dest => dest.Firstname, opt => opt.MapFrom(src => src.Firstname))
        //    .ForMember(dest => dest.Lastname, opt => opt.MapFrom(src => src.Lastname))
        //    .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
        //    .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate));

        //// Guide → GuideViewModel
        //CreateMap<Guide, GuideViewModel>()
        //    .ForMember(dest => dest.GuideId, opt => opt.MapFrom(src => src.GuideId))
        //    .ForMember(dest => dest.Firstname, opt => opt.MapFrom(src => src.Firstname))
        //    .ForMember(dest => dest.Lastname, opt => opt.MapFrom(src => src.Lastname))
        //    .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
        //    .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
        //    .ForMember(dest => dest.Trips, opt => opt.Ignore());

        //// GuideViewModel → Guide
        //CreateMap<GuideViewModel, Guide>()
        //    .ForMember(dest => dest.GuideId, opt => opt.MapFrom(src => src.GuideId))
        //    .ForMember(dest => dest.Firstname, opt => opt.MapFrom(src => src.Firstname))
        //    .ForMember(dest => dest.Lastname, opt => opt.MapFrom(src => src.Lastname))
        //    .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
        //    .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
        //    .ForMember(dest => dest.Trips, opt => opt.Ignore());
    }
}
