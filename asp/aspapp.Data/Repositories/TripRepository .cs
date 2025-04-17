using aspapp.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace aspapp.Data.Repositories
{
    public class TripRepository : ITripRepository
    {
        private readonly trip_context _context;

        public TripRepository(trip_context context)
        {
            _context = context;
        }

        public IQueryable<Trip> GetAllTrips()
        {
            return _context.Trips
                .Include(t => t.Guide)
                .Include(t => t.Travelers);
        }

        public async Task<Trip> GetTripById(int tripId)
        {
            return await _context.Trips
                .Include(t => t.Guide)
                .Include(t => t.Travelers)
                .FirstOrDefaultAsync(t => t.Id == tripId);
        }


        public async Task AddTrip(Trip trip)
        {
            await _context.Trips.AddAsync(trip);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTrip(Trip trip)
        {
            _context.Trips.Update(trip);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTrip(int tripId)
        {
            var trip = await _context.Trips.FindAsync(tripId);
            if (trip != null)
            {
                _context.Trips.Remove(trip);
                await _context.SaveChangesAsync();
            }
        }
    }
}
