using aspapp.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace aspapp.Data.Repositories
{
    public class GuideRepository : IGuideRepository
    {
        private readonly trip_context _context;

        public GuideRepository(trip_context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Guide>> GetAllGuides()
        {
            return await _context.Guides.ToListAsync();
        }

        public async Task<Guide> GetGuideById(int guideId)
        {
            return await _context.Guides.FindAsync(guideId);
        }

        public async Task AddGuide(Guide guide)
        {
            await _context.Guides.AddAsync(guide);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateGuide(Guide guide)
        {
            _context.Guides.Update(guide);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteGuide(int guideId)
        {
            var guide = await _context.Guides.FindAsync(guideId);
            if (guide != null)
            {
                _context.Guides.Remove(guide);
                await _context.SaveChangesAsync();
            }
        }
    }
}
