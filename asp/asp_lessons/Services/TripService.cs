//using aspapp.Models;
//using aspapp.Repositories;
//using aspapp.Services;

//using aspapp.Models.Validator;
//using aspapp.Models.VM;
//using AutoMapper;

//public class TripService : ITripService
//{
//    private readonly IMapper _mapper;
//    private readonly ITripRepository _tripRepository;

//    public TripService(ITripRepository tripRepository, IMapper mapper)
//    {
//        _tripRepository = tripRepository;
//        _mapper = mapper;
//    }

//    public IQueryable<TripViewModel> GetAllTrips()
//    {
//        var trips = _tripRepository.GetAllTrips().ToList(); 
//        return _mapper.Map<List<TripViewModel>>(trips).AsQueryable();
//    }

//    public async Task<TripViewModel> GetTripById(int tripId)
//    {
//        var trip = await _tripRepository.GetTripById(tripId);
//        if (trip == null)
//        {
//            throw new Exception("Trip not found");
//        }
//        return _mapper.Map<TripViewModel>(trip);
//    }

//    public async Task AddTrip(TripViewModel tripVm)
//    {
//        if (string.IsNullOrEmpty(tripVm.Title) ||
//            string.IsNullOrEmpty(tripVm.Description) ||
//            tripVm.GuideId <= 0 || tripVm.TravelerId <= 0)
//        {
//            throw new Exception("All fields are required.");
//        }

//        var trip = _mapper.Map<Trip>(tripVm);
//        await _tripRepository.AddTrip(trip);
//    }

//    public async Task UpdateTrip(TripViewModel tripVm)
//    {
//        if (string.IsNullOrEmpty(tripVm.Title) ||
//            string.IsNullOrEmpty(tripVm.Description) ||
//            tripVm.GuideId <= 0 || tripVm.TravelerId <= 0)
//        {
//            throw new Exception("All fields are required.");
//        }

//        var trip = _mapper.Map<Trip>(tripVm);
//        await _tripRepository.UpdateTrip(trip);
//    }

//    public async Task DeleteTrip(int tripId)
//    {
//        var trip = await _tripRepository.GetTripById(tripId);
//        if (trip == null)
//        {
//            throw new Exception("Trip not found.");
//        }

//        await _tripRepository.DeleteTrip(tripId);
//    }
//}
