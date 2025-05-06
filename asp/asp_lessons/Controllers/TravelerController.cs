using Microsoft.AspNetCore.Mvc;
using aspapp.Models;
using aspapp.Repositories;
using aspapp.Services;
using System.Threading.Tasks;
using System.Collections.Generic;
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


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var travelers = await _travelerService.GetAllTravelers()
                                                  .OrderBy(t => t.Firstname)
                                                  .ToListAsync();

            return View(travelers);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Firstname,Lastname,Email,BirthDate")] Traveler traveler)
        {
            if (ModelState.IsValid)
            {
                await _travelerService.AddTraveler(traveler);
                return RedirectToAction(nameof(Index));
            }
            return View(traveler);
        }

        [HttpGet("edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var traveler = await _travelerService.GetTravelerById(id);
            if (traveler == null)
            {
                return NotFound();
            }
            return View(traveler);
        }

        [HttpPost("edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TravelerId,Firstname,Lastname,Email,BirthDate")] Traveler traveler)
        {
            if (id != traveler.Id) 
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _travelerService.UpdateTraveler(traveler);
                return RedirectToAction(nameof(Index));
            }
            return View(traveler);
        }


        [HttpGet("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var traveler = await _travelerService.GetTravelerById(id);
            if (traveler == null)
            {
                return NotFound();
            }
            return View(traveler);
        }

        [HttpPost("delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _travelerService.DeleteTraveler(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
