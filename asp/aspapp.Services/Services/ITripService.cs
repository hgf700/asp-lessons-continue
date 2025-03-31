using aspapp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aspapp.Services.Services
{
    public interface ITripService
    {
        Task<IEnumerable<Trip>> GetAllTrips();
        Task<Trip> GetTripById(int tripId);
        Task AddTrip(Trip trip);
        Task UpdateTrip(Trip trip);
        Task DeleteTrip(int tripId);
    }
}
