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
        private readonly trip_context _context;

        public TripRepository(trip_context context)
        {
            _context = context;
        }

        // Pobiera wszystkie podróże z dołączonymi przewodnikami i podróżnikami
        public IQueryable<Trip> GetAllTrips()
        {
            return _context.Trips
                .Include(t => t.Guides)
                .Include(t => t.Travelers);
        }

        // Pobiera jedną podróż po ID z dołączonymi kolekcjami
        public async Task<Trip?> GetTripById(int id)
        {
            return await _context.Trips
                .Include(t => t.Guides)
                .Include(t => t.Travelers)
                .FirstOrDefaultAsync(t => t.TripId == id);
        }

        // Dodaje nową podróż
        public async Task AddTrip(Trip trip)
        {
            if (trip == null)
                throw new ArgumentNullException(nameof(trip));

            _context.Trips.Add(trip);
            await _context.SaveChangesAsync();
        }

        // Aktualizuje istniejącą podróż oraz powiązania
        public async Task UpdateTrip(Trip trip)
        {
            if (trip == null)
                throw new ArgumentNullException(nameof(trip));

            var existingTrip = await _context.Trips
                .Include(t => t.Guides)
                .Include(t => t.Travelers)
                .FirstOrDefaultAsync(t => t.TripId == trip.TripId);

            if (existingTrip == null)
                throw new KeyNotFoundException($"Trip with Id {trip.TripId} not found");

            // Aktualizacja podstawowych pól
            _context.Entry(existingTrip).CurrentValues.SetValues(trip);

            // Synchronizacja kolekcji Travelers
            existingTrip.Travelers.Clear();
            foreach (var traveler in trip.Travelers)
            {
                _context.Attach(traveler);
                existingTrip.Travelers.Add(traveler);
            }

            // Synchronizacja kolekcji Guides
            existingTrip.Guides.Clear();
            foreach (var guide in trip.Guides)
            {
                _context.Attach(guide);
                existingTrip.Guides.Add(guide);
            }

            await _context.SaveChangesAsync();
        }

        // Usuwa podróż po ID
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
