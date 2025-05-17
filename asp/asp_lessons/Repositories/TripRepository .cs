using aspapp.Models;
using Microsoft.EntityFrameworkCore;

namespace aspapp.Repositories
{
    public class TripRepository : ITripRepository
    {
        private readonly TripContext _context;

        public TripRepository(TripContext context)
        {
            _context = context;
        }

        // Zwraca wszystkie wycieczki z przewodnikiem i podróżnikiem
        public IQueryable<Trip> GetAllTrips()
        {
            return _context.Trips
                .Include(t => t.Guide)
                .Include(t => t.Traveler);
        }

        // Zwraca konkretną wycieczkę po ID wraz z powiązaniami
        public async Task<Trip?> GetTripById(int id)
        {
            return await _context.Trips
                .Include(t => t.Guide)
                .Include(t => t.Traveler)
                .FirstOrDefaultAsync(t => t.TripId == id);
        }

        // Dodaje nową wycieczkę do bazy
        public async Task AddTrip(Trip trip)
        {
            if (trip == null)
                throw new ArgumentNullException(nameof(trip));

            // Zakładamy, że Guide i Traveler już istnieją w bazie
            _context.Guides.Attach(trip.Guide);
            _context.Travelers.Attach(trip.Traveler);

            _context.Trips.Add(trip);
            await _context.SaveChangesAsync();
        }

        // Aktualizuje istniejącą wycieczkę
        public async Task UpdateTrip(Trip trip)
        {
            if (trip == null)
                throw new ArgumentNullException(nameof(trip));

            var existingTrip = await _context.Trips
                .FirstOrDefaultAsync(t => t.TripId == trip.TripId);

            if (existingTrip == null)
                throw new KeyNotFoundException($"Trip with Id {trip.TripId} not found");

            // Update podstawowych danych
            existingTrip.Destination = trip.Destination;
            existingTrip.StartDate = trip.StartDate;
            existingTrip.EndDate = trip.EndDate;

            // Aktualizacja relacji do przewodnika
            existingTrip.GuideId = trip.GuideId;
            _context.Attach(trip.Guide);
            existingTrip.Guide = trip.Guide;

            // Aktualizacja relacji do podróżnika
            existingTrip.TravelerId = trip.TravelerId;
            _context.Attach(trip.Traveler);
            existingTrip.Traveler = trip.Traveler;

            await _context.SaveChangesAsync();
        }

        // Usunięcie wycieczki
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
