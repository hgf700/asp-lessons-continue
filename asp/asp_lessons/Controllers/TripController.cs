using aspapp.Models;
using aspapp.Models.VM;
using aspapp.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace aspapp.Controllers
{
    [Route("trip")]
    public class TripController : Controller
    {
        private readonly ITripRepository _tripRepository;
        private readonly IGuideRepository _guideRepository;
        private readonly ITravelerRepository _travelerRepository;
        private readonly ILogger<TripController> _logger;
        private readonly IMapper _mapper;

        public TripController(
            ITripRepository tripRepository,
            IGuideRepository guideRepository,
            ITravelerRepository travelerRepository,
            ILogger<TripController> logger,
            IMapper mapper)
        {
            _tripRepository = tripRepository;
            _guideRepository = guideRepository;
            _travelerRepository = travelerRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("create")]
        public async Task<IActionResult> Create()
        {
            var viewModel = new TripViewModel
            {
                Guides = await _guideRepository.GetAllGuides().ToListAsync(),
                Travelers = await _travelerRepository.GetAllTravelers().ToListAsync()
            };

            return View(viewModel);
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TripViewModel model)
        {
            _logger.LogInformation("Wywołano akcję Create");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("ModelState jest niepoprawny");
                model.Guides = await _guideRepository.GetAllGuides().ToListAsync();
                model.Travelers = await _travelerRepository.GetAllTravelers().ToListAsync();
                return View(model);
            }

            var trip = new Trip
            {
                Title = model.Title,
                Description = model.Description,
                GuideId = model.GuideId,
                Travelers = await _travelerRepository
                    .GetAllTravelers()
                    .Where(t => model.TravelerIds.Contains(t.Id))
                    .ToListAsync()
            };

            await _tripRepository.AddTrip(trip);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var trips = await _tripRepository.GetAllTrips().ToListAsync();
            return View(trips);
        }

        [HttpGet("edit")]
        public async Task<IActionResult> EditTrip(int id)
        {
            var trip = await _tripRepository.GetTripById(id);
            if (trip == null)
            {
                return NotFound();
            }

            var viewModel = new TripViewModel
            {
                Id = trip.Id,
                Title = trip.Title,
                Description = trip.Description,
                GuideId = trip.GuideId,
                TravelerIds = trip.Travelers.Select(t => t.Id).ToList(),
                Guides = await _guideRepository.GetAllGuides().ToListAsync(),
                Travelers = await _travelerRepository.GetAllTravelers().ToListAsync()
            };

            return View(viewModel);
        }

        [HttpPost("edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTrip(int id, TripViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Guides = await _guideRepository.GetAllGuides().ToListAsync();
                model.Travelers = await _travelerRepository.GetAllTravelers().ToListAsync();
                return View(model);
            }

            var trip = await _tripRepository.GetTripById(id);
            if (trip == null)
            {
                return NotFound();
            }

            trip.Title = model.Title;
            trip.Description = model.Description;
            trip.GuideId = model.GuideId;
            trip.Travelers = await _travelerRepository
                .GetAllTravelers()
                .Where(t => model.TravelerIds.Contains(t.Id))
                .ToListAsync();

            await _tripRepository.UpdateTrip(trip);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("delete")]
        public async Task<IActionResult> DeleteTrip(int id)
        {
            var trip = await _tripRepository.GetTripById(id);
            if (trip == null)
            {
                return NotFound();
            }

            return View(trip);
        }

        [HttpPost("delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteTripConfirmed(int id)
        {
            await _tripRepository.DeleteTrip(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
