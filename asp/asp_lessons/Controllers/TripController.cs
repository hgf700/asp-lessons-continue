using aspapp.Models;
using aspapp.Models.VM;
using aspapp.Repositories;
using aspapp.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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

        public TripController(ITripRepository tripRepository,
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
            _logger.LogInformation("_LOGGER_ Wywołano akcję Create");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("_LOGGER_ ModelState jest niepoprawny");

                model.Guides = await _guideRepository.GetAllGuides().ToListAsync();
                model.Travelers = await _travelerRepository.GetAllTravelers().ToListAsync();
                return View(model);
            }

            _logger.LogInformation("_LOGGER_ Tworzenie nowej podróży. GuideId: {GuideId}, TravelerId: {TravelerId}",
                                   model.GuideId, model.TravelerId);


            var trip = _mapper.Map<Trip>(model);

            //var config = new MapperConfiguration(cfg => cfg.AddProfile<TripMapper>());
            //var mapper = config.CreateMapper();
            //var trip = mapper.Map<Trip>(model);


            await _tripRepository.AddTrip(trip);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var trips =  _tripRepository.GetAllTrips().ToListAsync();
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
                GuideId = trip.GuideId.Value,
                TravelerId = trip.TravelerId.Value,
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

        [HttpPost ("delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteTripConfirmed(int id)
        {
            await _tripRepository.DeleteTrip(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
