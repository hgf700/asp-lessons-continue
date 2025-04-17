using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aspapp.Data.Models;

namespace aspapp.Services.Services
{
    public interface IGuideService
    {
        IQueryable<Guide> GetAllGuides();
        Task<Guide> GetGuideDetails(int guideId);
        Task AddGuide(Guide guide);
        Task UpdateGuide(Guide guide);
        Task DeleteGuide(int id);
    }

}
