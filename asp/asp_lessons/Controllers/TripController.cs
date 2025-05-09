//using aspapp.Models;
//using aspapp.Models.VM;
//using aspapp.Services;
//using AutoMapper;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace aspapp.Controllers
//{
//    [Route("trip")]
//    public class TripController : Controller
//    {
//        private readonly ITripService _tripService;
//        private readonly IGuideService _guideService;
//        private readonly ITravelerService _travelerService;
//        private readonly IMapper _mapper;

//        public TripController(ITripService tripService, IGuideService guideService, ITravelerService travelerService, IMapper mapper)
//        {
//            _tripService = tripService;
//            _guideService = guideService;
//            _travelerService = travelerService;
//            _mapper = mapper;
//        }

//        [HttpGet("")]
//        public async Task<IActionResult> Index()
//        {
//            var trips = await _tripService.GetAllTrips().ToListAsync();
//            return View(trips);
//        }
//        [HttpGet("create")]
//        public async Task<IActionResult> Create()
//        {
//            var vm = new TripViewModel
//            {
//                // Mapowanie GuideViewModel na Guide
//                Guides = await _guideService.GetAllGuides()
//                    .Select(g => _mapper.Map<GuideViewModel>(g))  // Mapowanie Guide do GuideViewModel
//                    .ToListAsync(),

//                Travelers = await _travelerService.GetAllTravelers()
//                    .Select(t => new TravelerViewModel { TravelerId = t.TravelerId, Firstname = t.Firstname, Lastname = t.Lastname })
//                    .ToListAsync()  // Mapowanie na TravelerViewModel
//            };

//            return View(vm);
//        }

//        [HttpPost("create")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create(TripViewModel vm)
//        {
//            if (!ModelState.IsValid)
//            {
//                // Mapowanie GuideViewModel na Guide po stronie błędu
//                vm.Guides = await _guideService.GetAllGuides()
//                    .Select(g => _mapper.Map<GuideViewModel>(g))
//                    .ToListAsync();

//                vm.Travelers = await _travelerService.GetAllTravelers()
//                    .Select(t => new TravelerViewModel { TravelerId = t.TravelerId, Firstname = t.Firstname, Lastname = t.Lastname })
//                    .ToListAsync();  // Mapowanie na TravelerViewModel

//                return View(vm);
//            }

//            var model = _mapper.Map<Trip>(vm);
//            await _tripService.AddTrip(model);

//            return RedirectToAction(nameof(Index));
//        }

//        [HttpGet("edit/{id}")]
//        public async Task<IActionResult> EditTrip(int id)
//        {
//            var trip = await _tripService.GetTripById(id);
//            if (trip == null) return NotFound();

//            var vm = _mapper.Map<TripViewModel>(trip);
//            vm.Guides = await _guideService.GetAllGuides().ToListAsync();
//            vm.Travelers = await _travelerService.GetAllTravelers()
//                .Select(t => new TravelerViewModel { TravelerId = t.TravelerId, Firstname = t.Firstname, Lastname = t.Lastname })
//                .ToListAsync();  // Mapowanie na TravelerViewModel

//            return View(vm);
//        }

//        [HttpPost("edit/{id}")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> EditTrip(int id, TripViewModel vm)
//        {
//            if (!ModelState.IsValid)
//            {
//                vm.Guides = await _guideService.GetAllGuides().ToListAsync();
//                vm.Travelers = await _travelerService.GetAllTravelers()
//                    .Select(t => new TravelerViewModel { TravelerId = t.TravelerId, Firstname = t.Firstname, Lastname = t.Lastname })
//                    .ToListAsync();  // Mapowanie na TravelerViewModel

//                return View(vm);
//            }

//            var model = _mapper.Map<Trip>(vm);
//            model.TripId = id;

//            await _tripService.UpdateTrip(model);

//            return RedirectToAction(nameof(Index));
//        }


//        [HttpGet("delete/{id}")]
//        public async Task<IActionResult> DeleteTrip(int id)
//        {
//            var trip = await _tripService.GetTripById(id);
//            return trip == null ? NotFound() : View(trip);
//        }

//        [HttpPost("delete/{id}")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteTripConfirmed(int id)
//        {
//            await _tripService.DeleteTrip(id);
//            return RedirectToAction(nameof(Index));
//        }
//    }
//}
