using aspapp.Models;
using Microsoft.EntityFrameworkCore;
using System;
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

        // Zwraca wszystkie wycieczki z przypisanym przewodnikiem i podróżnikami
        public IQueryable<Trip> GetAllTrips()
        {
            return _context.Trips
                .Include(t => t.Guide)
                .Include(t => t.Travelers);
        }

        // Zwraca konkretną wycieczkę po ID wraz z powiązaniami
        public async Task<Trip?> GetTripById(int id)
        {
            return await _context.Trips
                .Include(t => t.Guide)
                .Include(t => t.Travelers)
                .FirstOrDefaultAsync(t => t.TripId == id);
        }

        // Dodaje nową wycieczkę do bazy
        // Dodajemy nową wycieczkę do bazy
        public async Task AddTrip(Trip trip)
        {
            if (trip == null)
                throw new ArgumentNullException(nameof(trip));

            // Attach the travelers to the context, if they're already tracked
            if (trip.Travelers != null && trip.Travelers.Any())
            {
                _context.Travelers.AttachRange(trip.Travelers);  // This ensures they are not inserted again
            }

            // Do not add the guide here, as it's already in the database
            if (trip.Guide != null)
            {
                _context.Guides.Attach(trip.Guide);  // Attach the guide, so it's not inserted again
            }

            // Add the trip to the context
            _context.Trips.Add(trip);
            await _context.SaveChangesAsync();
        }


    }
}
