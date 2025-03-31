using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aspapp.Data.Models;
using aspapp.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace aspapp.Services.Services
{
    public class GuideService : IGuideService
    {
        private readonly IGuideRepository _guideRepository;

        public GuideService(IGuideRepository guideRepository)
        {
            _guideRepository = guideRepository;
        }

        public async Task<IEnumerable<Guide>> GetAllGuides()
        {
            return await _guideRepository.GetAllGuides();
        }

        public async Task<Guide> GetGuideDetails(int guideId)
        {
            var guide = await _guideRepository.GetGuideById(guideId);
            if (guide == null)
            {
                throw new Exception("Guide not found");
            }
            return guide;
        }

        public async Task AddGuide(Guide guide)
        {
            if (string.IsNullOrEmpty(guide.Firstname) ||
                string.IsNullOrEmpty(guide.Email) ||
                string.IsNullOrEmpty(guide.Lastname) ||
                guide.Title == null)
            {
                throw new Exception("All fields are required.");
            }

            await _guideRepository.AddGuide(guide);
        }

        public async Task UpdateGuide(Guide guide)
        {
            if (string.IsNullOrEmpty(guide.Firstname) ||
                string.IsNullOrEmpty(guide.Email) ||
                string.IsNullOrEmpty(guide.Lastname) ||
                guide.Title == null)
            {
                throw new Exception("All fields are required.");
            }

            await _guideRepository.UpdateGuide(guide);
        }

        public async Task DeleteGuide(int id)
        {
            var guide = await _guideRepository.GetGuideById(id);
            if (guide == null)
            {
                throw new Exception("Guide not found.");
            }

            await _guideRepository.DeleteGuide(id);
        }
    }
}
