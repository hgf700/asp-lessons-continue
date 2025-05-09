using aspapp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspapp.Repositories
{
    public class GuideRepository : IGuideRepository
    {
        private readonly trip_context _context;

        public GuideRepository(trip_context context)
        {
            _context = context;
        }

        // Pobiera wszystkich przewodników (bez śledzenia zmian - lepsza wydajność)
        public IQueryable<Guide> GetAllGuides(bool includeTrips = false)
        {
            var query = _context.Guides.AsQueryable();

            // Jeśli chcesz dołączyć podróże, używamy Include
            if (includeTrips)
            {
                query = query.Include(g => g.Trips);
            }

            return query.AsNoTracking();
        }

        // Pobiera przewodnika po ID (wraz z powiązanymi wycieczkami)
        public async Task<Guide?> GetGuideById(int guideId, bool includeTrips = false)
        {
            var query = _context.Guides.AsQueryable();

            // Jeśli chcesz dołączyć podróże, używamy Include
            if (includeTrips)
            {
                query = query.Include(g => g.Trips);
            }

            return await query.AsNoTracking()
                               .FirstOrDefaultAsync(g => g.GuideId == guideId);
        }

        // Dodaje nowego przewodnika
        public async Task AddGuide(Guide guide)
        {
            // Sprawdzamy, czy przewodnik o tym samym emailu już istnieje
            var existingGuide = await _context.Guides
                .FirstOrDefaultAsync(g => g.Email == guide.Email);

            if (existingGuide != null)
            {
                throw new InvalidOperationException("Przewodnik z tym emailem już istnieje.");
            }

            await _context.Guides.AddAsync(guide);
            await _context.SaveChangesAsync();
        }

        // Aktualizuje dane przewodnika po sprawdzeniu istnienia
        public async Task UpdateGuide(Guide guide)
        {
            var existingGuide = await _context.Guides.FindAsync(guide.GuideId);
            if (existingGuide == null)
            {
                throw new KeyNotFoundException("Przewodnik nie został znaleziony.");
            }

            // Zaktualizowanie wartości istniejącego przewodnika
            _context.Entry(existingGuide).CurrentValues.SetValues(guide);
            await _context.SaveChangesAsync();
        }

        // Usuwa przewodnika
        public async Task DeleteGuide(int guideId)
        {
            var guide = await _context.Guides.FindAsync(guideId);
            if (guide == null)
            {
                throw new KeyNotFoundException("Przewodnik nie został znaleziony.");
            }

            _context.Guides.Remove(guide);
            await _context.SaveChangesAsync();
        }
    }
}
