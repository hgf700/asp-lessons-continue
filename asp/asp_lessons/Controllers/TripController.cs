using aspapp.Data.Models;
using aspapp.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using aspapp.Data.Models.VM;

namespace aspapp.Controllers
{
    public class TripController : Controller
    {
        private readonly ITripRepository _tripRepository;
        private readonly IGuideRepository _guideRepository;
        private readonly ITravelerRepository _travelerRepository;

        public TripController(ITripRepository tripRepository, IGuideRepository guideRepository, ITravelerRepository travelerRepository)
        {
            _tripRepository = tripRepository;
            _guideRepository = guideRepository;
            _travelerRepository = travelerRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var viewModel = new TripViewModel
            {
                Guides = await _guideRepository.GetAllGuides().ToListAsync(),
                Travelers = await _travelerRepository.GetAllTravelers().ToListAsync()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TripViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Przekaż dane do widoku w przypadku nieprawidłowego formularza
                model.Guides = await _guideRepository.GetAllGuides().ToListAsync();
                model.Travelers = await _travelerRepository.GetAllTravelers().ToListAsync();
                return View(model);
            }

            var trip = new Trip
            {
                Title = model.Title,
                Description = model.Description,
                GuideId = model.GuideId,
                TravelerId = model.TravelerId
            };

            await _tripRepository.AddTrip(trip);
            return RedirectToAction(nameof(Index));  // Przekierowanie na stronę główną
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var trips =  _tripRepository.GetAllTrips().ToListAsync();
            return View(trips);
        }

        [HttpGet]
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
                TravelerId = trip.TravelerId,
                Guides = await _guideRepository.GetAllGuides().ToListAsync(),  
                Travelers = await _travelerRepository.GetAllTravelers().ToListAsync()  
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTrip(int id, TripViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Pobieramy pełne listy Guide i Traveler
                model.Guides = await _guideRepository.GetAllGuides().ToListAsync();
                model.Travelers = await _travelerRepository.GetAllTravelers().ToListAsync();

                return View(model);
            }

            var trip = await _tripRepository.GetTripById(id);
            if (trip == null)
            {
                return NotFound();
            }

            // Aktualizujemy dane podróży
            trip.Title = model.Title;
            trip.Description = model.Description;
            trip.GuideId = model.GuideId;
            trip.TravelerId = model.TravelerId;

            await _tripRepository.UpdateTrip(trip);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> DeleteTrip(int id)
        {
            var trip = await _tripRepository.GetTripById(id);
            if (trip == null)
            {
                return NotFound();
            }
            return View(trip);
        }

        [HttpPost, ActionName("DeleteTrip")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteTripConfirmed(int id)
        {
            await _tripRepository.DeleteTrip(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
