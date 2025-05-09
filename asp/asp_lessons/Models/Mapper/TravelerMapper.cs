using AutoMapper;
using aspapp.Models;
using aspapp.Models.VM;

namespace aspapp.Profiles
{
    public class TravelerProfile : Profile
    {
        public TravelerProfile()
        {
            // Mapowanie między Traveler a TravelerViewModel
            CreateMap<Traveler, TravelerViewModel>()
                .ForMember(dest => dest.TravelerId, opt => opt.MapFrom(src => src.TravelerId))
                .ForMember(dest => dest.Firstname, opt => opt.MapFrom(src => src.Firstname))
                .ForMember(dest => dest.Lastname, opt => opt.MapFrom(src => src.Lastname))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
                .ForMember(dest => dest.Trips, opt => opt.MapFrom(src => src.Trips));

            // Mapowanie między TravelerViewModel a Traveler
            CreateMap<TravelerViewModel, Traveler>()
                .ForMember(dest => dest.TravelerId, opt => opt.MapFrom(src => src.TravelerId))
                .ForMember(dest => dest.Firstname, opt => opt.MapFrom(src => src.Firstname))
                .ForMember(dest => dest.Lastname, opt => opt.MapFrom(src => src.Lastname))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
                .ForMember(dest => dest.Trips, opt => opt.MapFrom(src => src.Trips));
        }
    }
}
