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
                .Include(t => t.Travelers)
                .ToListAsync();

            // Map the trips to TripViewModel
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

            // Fetch the guide (without trying to insert it again)
            var guide = await _guideRepository.GetGuideById(tripViewModel.GuideId);
            if (guide == null)
                throw new KeyNotFoundException($"Guide with Id {tripViewModel.GuideId} not found.");

            // Fetch selected travelers
            var travelers = new List<Traveler>();
            foreach (var travelerId in tripViewModel.SelectedTravelerIds)
            {
                var traveler = await _travelerRepository.GetTravelerById(travelerId);
                if (traveler == null)
                    throw new KeyNotFoundException($"Traveler with Id {travelerId} not found.");

                travelers.Add(traveler);
            }

            // Map the Trip entity
            var trip = _mapper.Map<Trip>(tripViewModel);

            // Do not set the guide as a new object, just assign the existing one
            trip.Guide = guide;

            // Assign the travelers
            trip.Travelers = travelers;

            // Add the new trip to the repository
            await _tripRepository.AddTrip(trip);
        }

        public async Task UpdateTrip(TripViewModel tripViewModel)
        {
            ValidateTripViewModel(tripViewModel);

            var existingTrip = await _tripRepository.GetTripById(tripViewModel.TripId);
            if (existingTrip == null)
                throw new KeyNotFoundException($"Trip with Id {tripViewModel.TripId} not found.");

            // Fetch the guide and travelers
            var guide = await _guideRepository.GetGuideById(tripViewModel.GuideId);
            if (guide == null)
                throw new KeyNotFoundException($"Guide with Id {tripViewModel.GuideId} not found.");

            var travelers = new List<Traveler>();
            foreach (var travelerVM in tripViewModel.Travelers)
            {
                var traveler = await _travelerRepository.GetTravelerById(travelerVM.TravelerId);
                if (traveler == null)
                    throw new KeyNotFoundException($"Traveler with Id {travelerVM.TravelerId} not found.");

                travelers.Add(traveler);
            }

            var updatedTrip = _mapper.Map<Trip>(tripViewModel);
            updatedTrip.Guide = guide;
            updatedTrip.Travelers = travelers;

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
