using aspapp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspapp.Repositories
{
    public class TripRepository : ITripRepository
    {
        private readonly TripContext _context;

        public TripRepository(TripContext context)
        {
            _context = context;
        }

        // Get all trips with the associated guide and travelers
        public IQueryable<Trip> GetAllTrips()
        {
            return _context.Trips
                .Include(t => t.Guide)
                .Include(t => t.Travelers);
        }

        // Get a trip by ID with associated guide and travelers
        public async Task<Trip?> GetTripById(int id)
        {
            return await _context.Trips
                .Include(t => t.Guide)
                .Include(t => t.Travelers)
                .FirstOrDefaultAsync(t => t.TripId == id);
        }

        // Add a new trip to the database
        public async Task AddTrip(Trip trip)
        {
            if (trip == null)
                throw new ArgumentNullException(nameof(trip));

            _context.Trips.Add(trip);
            await _context.SaveChangesAsync();
        }

        // Update an existing trip and its related entities
        public async Task UpdateTrip(Trip trip)
        {
            if (trip == null)
                throw new ArgumentNullException(nameof(trip));

            var existingTrip = await _context.Trips
                .Include(t => t.Guide)
                .Include(t => t.Travelers)
                .FirstOrDefaultAsync(t => t.TripId == trip.TripId);

            if (existingTrip == null)
                throw new KeyNotFoundException($"Trip with Id {trip.TripId} not found");

            // Update the basic fields
            _context.Entry(existingTrip).CurrentValues.SetValues(trip);

            // Update guide
            existingTrip.GuideId = trip.GuideId;
            _context.Attach(trip.Guide);
            existingTrip.Guide = trip.Guide;

            // Sync Travelers collection
            existingTrip.Travelers.Clear();
            foreach (var traveler in trip.Travelers)
            {
                _context.Attach(traveler);
                existingTrip.Travelers.Add(traveler);
            }

            await _context.SaveChangesAsync();
        }

        // Delete a trip by its ID
        public async Task DeleteTrip(int id)
        {
            var trip = await _context.Trips.FindAsync(id);
            if (trip == null)
                throw new KeyNotFoundException($"Trip with Id {id} not found");

            _context.Trips.Remove(trip);
            await _context.SaveChangesAsync();
        }
    }
}
