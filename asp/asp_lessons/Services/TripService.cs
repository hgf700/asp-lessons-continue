using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using aspapp.Models.VM;
using aspapp.Models;
using aspapp.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace aspapp.Services
{
    public class TripService : ITripService
    {
        private readonly ITripRepository _tripRepository;
        private readonly IMapper _mapper;
        private readonly ITravelerRepository _travelerRepository;
        private readonly IGuideRepository _guideRepository;

        public TripService(
            ITripRepository tripRepository,
            IMapper mapper,
            ITravelerRepository travelerRepository,
            IGuideRepository guideRepository)
        {
            _tripRepository = tripRepository;
            _mapper = mapper;
            _travelerRepository = travelerRepository;
            _guideRepository = guideRepository;
        }

        public async Task<List<TripViewModel>> GetAllTrips()
        {
            var trips = await _tripRepository.GetAllTrips()
                .Include(t => t.Guide)
                .Include(t => t.Traveler)
                .ToListAsync();

            return trips.Select(trip => _mapper.Map<TripViewModel>(trip)).ToList();
        }

        public async Task<TripViewModel> GetTripById(int tripId)
        {
            var trip = await _tripRepository.GetTripById(tripId);
            if (trip == null)
                throw new KeyNotFoundException($"Trip with Id {tripId} not found.");

            return _mapper.Map<TripViewModel>(trip);
        }

        public async Task AddTrip(TripViewModel tripViewModel)
        {
            ValidateTripViewModel(tripViewModel);

            var trip = _mapper.Map<Trip>(tripViewModel);

            if (tripViewModel.GuideId.HasValue)
            {
                var guide = await _guideRepository.GetGuideById(tripViewModel.GuideId.Value)
                             ?? throw new KeyNotFoundException($"Guide with Id {tripViewModel.GuideId.Value} not found.");
                trip.Guide = guide;
            }

            if (tripViewModel.TravelerId.HasValue)
            {
                var traveler = await _travelerRepository.GetTravelerById(tripViewModel.TravelerId.Value)
                                ?? throw new KeyNotFoundException($"Traveler with Id {tripViewModel.TravelerId.Value} not found.");
                trip.Traveler = traveler;
            }

            await _tripRepository.AddTrip(trip);
        }

        public async Task UpdateTrip(TripViewModel tripViewModel)
        {
            ValidateTripViewModel(tripViewModel);

            var existingTrip = await _tripRepository.GetTripById(tripViewModel.TripId);
            if (existingTrip == null)
                throw new KeyNotFoundException($"Trip with Id {tripViewModel.TripId} not found.");

            var updatedTrip = _mapper.Map<Trip>(tripViewModel);

            if (tripViewModel.GuideId.HasValue)
            {
                var guide = await _guideRepository.GetGuideById(tripViewModel.GuideId.Value)
                             ?? throw new KeyNotFoundException($"Guide with Id {tripViewModel.GuideId.Value} not found.");
                updatedTrip.Guide = guide;
            }
            else
            {
                updatedTrip.Guide = null;
            }

            if (tripViewModel.TravelerId.HasValue)
            {
                var traveler = await _travelerRepository.GetTravelerById(tripViewModel.TravelerId.Value)
                                ?? throw new KeyNotFoundException($"Traveler with Id {tripViewModel.TravelerId.Value} not found.");
                updatedTrip.Traveler = traveler;
            }
            else
            {
                updatedTrip.Traveler = null;
            }

            await _tripRepository.UpdateTrip(updatedTrip);
        }

        public async Task DeleteTrip(int tripId)
        {
            var trip = await _tripRepository.GetTripById(tripId);
            if (trip == null)
                throw new KeyNotFoundException($"Trip with Id {tripId} not found.");

            await _tripRepository.DeleteTrip(tripId);
        }

        private void ValidateTripViewModel(TripViewModel viewModel)
        {
            var context = new ValidationContext(viewModel);
            var results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(viewModel, context, results, true);

            if (!isValid)
                throw new ArgumentException(string.Join(", ", results.Select(r => r.ErrorMessage)));

            if (viewModel.EndDate < viewModel.StartDate)
                throw new ArgumentException("EndDate must be greater than or equal to StartDate.");
        }
    }
}
