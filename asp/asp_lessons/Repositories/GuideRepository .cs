using aspapp.Models;
using aspapp.Repositories;
using aspapp.Services;
using aspapp.Models.Mapper;
using aspapp.Models.Validator;
using aspapp.Models.VM;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        public IQueryable<Guide> GetAllGuides()
        {
            return _context.Guides;
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
