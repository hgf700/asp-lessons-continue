using Microsoft.AspNetCore.Mvc;
using aspapp.Models;
using aspapp.Services;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using aspapp.Models.VM;

namespace aspapp.Controllers
{
    [Route("trip")]
    public class TripController : Controller
    {
        private readonly ITripService _tripService;
        private readonly IGuideService _guideService;
        private readonly ITravelerService _travelerService;

        public TripController(ITripService tripService, IGuideService guideService, ITravelerService travelerService)
        {
            _tripService = tripService;
            _guideService = guideService;
            _travelerService = travelerService;
        }

        // Index GET
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var trips = await _tripService.GetAllTrips(); // Get all trips with associated data
            return View(trips);
        }

        // Create GET
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

        // Create POST
        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TripViewModel tripViewModel)
        {
            if (ModelState.IsValid)
            {
                await _tripService.AddTrip(tripViewModel);
                return RedirectToAction(nameof(Index));
            }
            // Reload the lists of guides and travelers in case of validation failure
            tripViewModel.Guides = await _guideService.GetAllGuides();
            tripViewModel.Travelers = await _travelerService.GetAllTravelers();
            return View(tripViewModel);
        }
    }
}
