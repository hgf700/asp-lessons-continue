using Microsoft.AspNetCore.Mvc;
using aspapp.Models;
using aspapp.Models.VM;
using aspapp.Services;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace aspapp.Controllers
{
    [Route("traveler")]
    public class TravelerController : Controller
    {
        private readonly ITravelerService _travelerService;

        public TravelerController(ITravelerService travelerService)
        {
            _travelerService = travelerService;
        }

        // Index GET
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var travelers = await _travelerService.GetAllTravelers();
            return View(travelers);
        }

        // Create GET
        [HttpGet("create")]
        public IActionResult Create() => View();

        // Create POST
        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Firstname,Lastname,Email,BirthDate")] TravelerViewModel traveler)
        {
            if (ModelState.IsValid)
            {
                await _travelerService.AddTraveler(traveler);
                return RedirectToAction(nameof(Index));
            }
            return View(traveler);
        }

        // Edit GET

    }
}
