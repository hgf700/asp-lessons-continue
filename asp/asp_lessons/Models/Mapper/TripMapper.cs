using aspapp.Models.VM;
using aspapp.Models;
using AutoMapper;

public class TripMapper : Profile
{
    public TripMapper()
    {
        // Trip -> TripViewModel
        CreateMap<Trip, TripViewModel>()
            .ForMember(dest => dest.GuideId, opt => opt.MapFrom(src => src.GuideId))
            .ForMember(dest => dest.TravelerIds, opt => opt.MapFrom(src => src.Travelers.Select(t => t.Id)))
            .ForMember(dest => dest.Guides, opt => opt.Ignore()) // będą ładowane osobno
            .ForMember(dest => dest.Travelers, opt => opt.Ignore());

        // TripViewModel -> Trip
        CreateMap<TripViewModel, Trip>()
            .ForMember(dest => dest.GuideId, opt => opt.MapFrom(src => src.GuideId))
            .ForMember(dest => dest.Travelers, opt => opt.Ignore()) // załadujesz ręcznie na podstawie ID
            .ForMember(dest => dest.Guide, opt => opt.Ignore()); // załadujesz ręcznie
    }
}
