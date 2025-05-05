using aspapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aspapp.Services
{
    public interface ITravelerService
    {
        IQueryable<Traveler> GetAllTravelers();
        Task<Traveler> GetTravelerById(int travelerId);
        Task AddTraveler(Traveler traveler);
        Task UpdateTraveler(Traveler traveler);
        Task DeleteTraveler(int travelerId);
    }
}
