using aspapp.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace aspapp.Data.Repositories
{
    public interface ITripRepository
    {
        Task<IEnumerable<Trip>> GetAllTrips();
        Task<Trip> GetTripById(int tripId);
        Task AddTrip(Trip trip);
        Task UpdateTrip(Trip trip);
        Task DeleteTrip(int tripId);
    }
}
