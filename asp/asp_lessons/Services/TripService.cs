using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using aspapp.Models.VM;
using aspapp.Models;
using aspapp.Repositories;
using AutoMapper;

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

        // Pobieranie wszystkich podróży
        public IQueryable<TripViewModel> GetAllTrips()
        {
            return _tripRepository.GetAllTrips()
                                  .Select(trip => _mapper.Map<TripViewModel>(trip));
        }

        // Pobieranie podróży po ID
        public async Task<TripViewModel> GetTripById(int tripId)
        {
            var trip = await _tripRepository.GetTripById(tripId);
            if (trip == null)
                throw new KeyNotFoundException($"Trip with Id {tripId} not found.");

            return _mapper.Map<TripViewModel>(trip);
        }

        // Dodawanie podróży
        public async Task AddTrip(TripViewModel tripViewModel)
        {
            ValidateTripViewModel(tripViewModel);

            // Walidacja istnienia przewodników
            foreach (var guideVM in tripViewModel.Guides)
            {
                var guide = await _guideRepository.GetGuideById(guideVM.GuideId);
                if (guide == null)
                    throw new KeyNotFoundException($"Guide with Id {guideVM.GuideId} not found.");
            }

            // Walidacja istnienia podróżników
            foreach (var travelerVM in tripViewModel.Travelers)
            {
                var traveler = await _travelerRepository.GetTravelerById(travelerVM.TravelerId);
                if (traveler == null)
                    throw new KeyNotFoundException($"Traveler with Id {travelerVM.TravelerId} not found.");
            }

            var trip = _mapper.Map<Trip>(tripViewModel);
            await _tripRepository.AddTrip(trip);
        }

        // Aktualizacja podróży
        public async Task UpdateTrip(TripViewModel tripViewModel)
        {
            ValidateTripViewModel(tripViewModel);

            var existingTrip = await _tripRepository.GetTripById(tripViewModel.TripId);
            if (existingTrip == null)
                throw new KeyNotFoundException($"Trip with Id {tripViewModel.TripId} not found.");

            // Walidacja istnienia przewodników
            foreach (var guideVM in tripViewModel.Guides)
            {
                var guide = await _guideRepository.GetGuideById(guideVM.GuideId);
                if (guide == null)
                    throw new KeyNotFoundException($"Guide with Id {guideVM.GuideId} not found.");
            }

            // Walidacja istnienia podróżników
            foreach (var travelerVM in tripViewModel.Travelers)
            {
                var traveler = await _travelerRepository.GetTravelerById(travelerVM.TravelerId);
                if (traveler == null)
                    throw new KeyNotFoundException($"Traveler with Id {travelerVM.TravelerId} not found.");
            }

            var updatedTrip = _mapper.Map<Trip>(tripViewModel);
            await _tripRepository.UpdateTrip(updatedTrip);
        }

        // Usuwanie podróży
        public async Task DeleteTrip(int tripId)
        {
            var trip = await _tripRepository.GetTripById(tripId);
            if (trip == null)
                throw new KeyNotFoundException($"Trip with Id {tripId} not found.");

            await _tripRepository.DeleteTrip(tripId);
        }

        // Walidacja danych podróży
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
