using aspapp.Models.VM;
using aspapp.Models;
using aspapp.Models.Mapper;
using AutoMapper;
using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aspapp.Tests
{
    public class TripMapperTests
    {
        private readonly IMapper _mapper;

        public TripMapperTests()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<TripMapper>());
            _mapper = config.CreateMapper();
        }

        [Fact]
        public void Should_Map_Trip_To_TripViewModel()
        {
            // Arrange
            var trip = new Trip
            {
                TripId = 1,
                Title = "Test Trip",
                Description = "Test Description",
                Guide = new Guide { GuideId = 1, Firstname = "John" },
                Travelers = new List<Traveler>
                {
                    new Traveler { TravelerId = 1, Firstname = "Alice" },
                    new Traveler { TravelerId = 2, Firstname = "Bob" }
                }
            };

            // Act
            var tripViewModel = _mapper.Map<TripViewModel>(trip);

            // Assert
            Assert.Equal(trip.Title, tripViewModel.Title);
            Assert.Equal(trip.Description, tripViewModel.Description);
            Assert.Equal("John", tripViewModel.GuideFirstname);
            Assert.Contains(1, tripViewModel.SelectedTravelerIds);
            Assert.Contains(2, tripViewModel.SelectedTravelerIds);
        }

        [Fact]
        public void Should_Map_TripViewModel_To_Trip()
        {
            // Arrange
            var tripViewModel = new TripViewModel
            {
                TripId = 1,
                Title = "Test Trip",
                Description = "Test Description",
                GuideId = 1,
                SelectedTravelerIds = new List<int> { 1, 2 }
            };

            // Act
            var trip = _mapper.Map<Trip>(tripViewModel);

            // Assert
            Assert.Equal(tripViewModel.Title, trip.Title);
            Assert.Equal(tripViewModel.Description, trip.Description);
            Assert.Equal(1, trip.Guide.GuideId);
            Assert.Contains(1, trip.Travelers.Select(t => t.TravelerId));
            Assert.Contains(2, trip.Travelers.Select(t => t.TravelerId));
        }
    }
}
