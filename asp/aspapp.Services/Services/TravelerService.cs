using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aspapp.Data.Models;
using aspapp.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace aspapp.Services.Services
{
    public class TravelerService : ITravelerService
    {
        private readonly ITravelerRepository _travelerRepository;

        public TravelerService(ITravelerRepository travelerRepository)
        {
            _travelerRepository = travelerRepository;
        }

        public async Task<IEnumerable<Traveler>> GetAllTravelers()
        {
            return await _travelerRepository.GetAllTravelers();
        }

        public async Task<Traveler> GetTravelerById(int travelerId)
        {
            var traveler = await _travelerRepository.GetTravelerById(travelerId);
            if (traveler == null)
            {
                throw new Exception("Traveler not found");
            }
            return traveler;
        }

        public async Task AddTraveler(Traveler traveler)
        {
            if (string.IsNullOrEmpty(traveler.Firstname) ||
                string.IsNullOrEmpty(traveler.Email) ||
                string.IsNullOrEmpty(traveler.Lastname))
            {
                throw new Exception("All fields are required.");
            }

            await _travelerRepository.AddTraveler(traveler);
        }

        public async Task UpdateTraveler(Traveler traveler)
        {
            if (string.IsNullOrEmpty(traveler.Firstname) ||
                string.IsNullOrEmpty(traveler.Email) ||
                string.IsNullOrEmpty(traveler.Lastname))
            {
                throw new Exception("All fields are required.");
            }

            await _travelerRepository.UpdateTraveler(traveler);
        }

        public async Task DeleteTraveler(int travelerId)
        {
            var traveler = await _travelerRepository.GetTravelerById(travelerId);
            if (traveler == null)
            {
                throw new Exception("Traveler not found.");
            }

            await _travelerRepository.DeleteTraveler(travelerId);
        }
    }
}
