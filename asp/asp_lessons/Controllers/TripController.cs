using aspapp.Data.Models;
using aspapp.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

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
        public async Task<IActionResult> CreateTrip()
        {
            ViewBag.Guides = new SelectList(await _guideRepository.GetAllGuides(), "Id", "Firstname");
            ViewBag.Travelers = new SelectList(await _travelerRepository.GetAllTravelers(), "Id", "Firstname");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTrip([Bind("Title,Description,GuideId,TravelerId")] Trip trip)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Guides = new SelectList(await _guideRepository.GetAllGuides(), "Id", "Firstname");
                ViewBag.Travelers = new SelectList(await _travelerRepository.GetAllTravelers(), "Id", "Firstname");
                return View(trip);
            }

            await _tripRepository.AddTrip(trip);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var trips = await _tripRepository.GetAllTrips();
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

            ViewBag.Guides = new SelectList(await _guideRepository.GetAllGuides(), "Id", "Firstname");
            ViewBag.Travelers = new SelectList(await _travelerRepository.GetAllTravelers(), "Id", "Firstname");

            return View(trip);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTrip(int id, [Bind("Title,Description,GuideId,TravelerId")] Trip trip)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Guides = new SelectList(await _guideRepository.GetAllGuides(), "Id", "Firstname");
                ViewBag.Travelers = new SelectList(await _travelerRepository.GetAllTravelers(), "Id", "Firstname");
                return View(trip);
            }

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
