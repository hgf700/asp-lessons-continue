using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aspapp.Data.Models;
using aspapp.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace aspapp.Services.Services
{

    public class TripService : ITripService
    {
        private readonly ITripRepository _tripRepository;

        public TripService(ITripRepository tripRepository)
        {
            _tripRepository = tripRepository;
        }

        public async Task<IEnumerable<Trip>> GetAllTrips()
        {
            return await _tripRepository.GetAllTrips();
        }

        public async Task<Trip> GetTripById(int tripId)
        {
            var trip = await _tripRepository.GetTripById(tripId);
            if (trip == null)
            {
                throw new Exception("Trip not found");
            }
            return trip;
        }

        public async Task AddTrip(Trip trip)
        {
            if (string.IsNullOrEmpty(trip.Title) ||
                string.IsNullOrEmpty(trip.Description) ||
                (trip.GuideId.HasValue && trip.GuideId <= 0))
            {
                throw new Exception("All fields are required.");
            }

            await _tripRepository.AddTrip(trip);
        }


        public async Task UpdateTrip(Trip trip)
        {
            if (string.IsNullOrEmpty(trip.Title) ||
                string.IsNullOrEmpty(trip.Description) ||
                (trip.GuideId.HasValue && trip.GuideId <= 0))
            {
                throw new Exception("All fields are required.");
            }

            await _tripRepository.UpdateTrip(trip);
        }

        public async Task DeleteTrip(int tripId)
        {
            var trip = await _tripRepository.GetTripById(tripId);
            if (trip == null)
            {
                throw new Exception("Trip not found.");
            }

            await _tripRepository.DeleteTrip(tripId);
        }
    }
}
