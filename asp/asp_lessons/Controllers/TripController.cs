using Microsoft.AspNetCore.Mvc;
using aspapp.Models;
using aspapp.Services;
using System.Threading.Tasks;
using aspapp.Models.VM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace aspapp.Controllers
{
    [Route("trip")]
    public class TripController : Controller
    {
        private readonly ITripService _tripService;
        private readonly IGuideService _guideService;
        private readonly ITravelerService _travelerService;
        private readonly UserManager<IdentityUser> _userManager;
        string[] roleNames = { "Admin", "Guide", "User" };

        public TripController(ITripService tripService, IGuideService guideService, ITravelerService travelerService, UserManager<IdentityUser> userManager)
        {
            _tripService = tripService;
            _guideService = guideService;
            _travelerService = travelerService;
            _userManager = userManager;
        }

        [Authorize(Roles = "Admin,Guide")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var trips = await _tripService.GetAllTrips(); // Get all trips with associated data
            return View(trips);
        }

        [Authorize(Roles = "Admin,Guide")]
        [HttpGet("create")]
        public async Task<IActionResult> Create()
        {
            // Prepare the guides and travelers lists for the form
            var guides = await _guideService.GetAllGuides();
            var travelers = await _travelerService.GetAllTravelers();
            var model = new TripViewModel
            {
                Guides = guides,
                Travelers = travelers
            };
            return View(model);
        }

        [Authorize(Roles = "Admin,Guide")]
        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TripViewModel tripViewModel)
        {
            if (ModelState.IsValid)
            {
                // Make sure SelectedTravelerIds is set, even if it's empty
                tripViewModel.SelectedTravelerIds ??= new List<int>();

                // Add the trip through the service
                await _tripService.AddTrip(tripViewModel);
                return RedirectToAction(nameof(Index));
            }

            // Reload the lists of guides and travelers in case of validation failure
            tripViewModel.Guides = await _guideService.GetAllGuides();
            tripViewModel.Travelers = await _travelerService.GetAllTravelers();
            return View(tripViewModel);
        }

        [Authorize(Roles = "Admin,Guide")]
        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var trip = await _tripService.GetTripById(id);
            if (trip == null)
            {
                return NotFound();
            }

            // Załaduj przewodników i podróżników ponownie
            trip.Guides = await _guideService.GetAllGuides();
            trip.Travelers = await _travelerService.GetAllTravelers();

            return View(trip);
        }

        [Authorize(Roles = "Admin,Guide")]
        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TripViewModel tripViewModel)
        {
            if (id != tripViewModel.TripId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                tripViewModel.SelectedTravelerIds ??= new List<int>();

                await _tripService.UpdateTrip(tripViewModel);
                return RedirectToAction(nameof(Index));
            }

            tripViewModel.Guides = await _guideService.GetAllGuides();
            tripViewModel.Travelers = await _travelerService.GetAllTravelers();
            return View(tripViewModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var trip = await _tripService.GetTripById(id);
            if (trip == null)
            {
                return NotFound();
            }

            return View(trip); // Confirmation view
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("delete/{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _tripService.DeleteTrip(id);
            return RedirectToAction(nameof(Index));
        }


    }
}
