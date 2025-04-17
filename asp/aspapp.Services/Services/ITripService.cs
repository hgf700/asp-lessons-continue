using aspapp.Data.Models;
using aspapp.Data.Models.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aspapp.Services.Services
{
    public interface ITripService
    {
        IQueryable<TripViewModel> GetAllTrips();
        Task<TripViewModel> GetTripById(int tripId);
        Task AddTrip(TripViewModel tripvm);
        Task UpdateTrip(TripViewModel tripvm);
        Task DeleteTrip(int tripId);
    }
}
