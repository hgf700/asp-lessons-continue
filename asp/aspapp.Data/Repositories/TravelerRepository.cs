using aspapp.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace aspapp.Data.Repositories
{
    public class TravelerRepository : ITravelerRepository
    {
        private readonly trip_context _context;

        public TravelerRepository(trip_context context)
        {
            _context = context;
        }

        public IQueryable<Traveler> GetAllTravelers()
        {
            return _context.Travelers;
        }

        public async Task<Traveler> GetTravelerById(int travelerId)
        {
            return await _context.Travelers.FindAsync(travelerId);
        }

        public async Task AddTraveler(Traveler traveler)
        {
            await _context.Travelers.AddAsync(traveler);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTraveler(Traveler traveler)
        {
            _context.Travelers.Update(traveler);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTraveler(int travelerId)
        {
            var traveler = await _context.Travelers.FindAsync(travelerId);
            if (traveler != null)
            {
                _context.Travelers.Remove(traveler);
                await _context.SaveChangesAsync();
            }
        }
    }
}
