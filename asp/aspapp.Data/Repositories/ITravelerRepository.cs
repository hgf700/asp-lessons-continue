using aspapp.Data.Models;

namespace aspapp.Data.Repositories
{
    public interface ITravelerRepository
    {
        Task<IEnumerable<Traveler>> GetAllTravelers();
        Task<Traveler> GetTravelerById(int travelerId);
        Task AddTraveler(Traveler traveler);
        Task UpdateTraveler(Traveler traveler);
        Task DeleteTraveler(int travelerId);
    }
}
