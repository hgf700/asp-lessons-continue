using aspapp.Data.Models;

namespace aspapp.Data.Repositories
{
    public interface IGuideRepository
    {
        IQueryable<Guide> GetAllGuides();
        Task<Guide> GetGuideById(int guideId);
        Task AddGuide(Guide guide);
        Task UpdateGuide(Guide guide);
        Task DeleteGuide(int guideId);
    }
}
