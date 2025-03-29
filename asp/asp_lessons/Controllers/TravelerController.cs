using Microsoft.AspNetCore.Mvc;
using aspapp.Data.Models;
using aspapp.Data.Repositories;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace aspapp.Controllers
{
    public class TravelerController : Controller
    {
        private readonly ITravelerRepository _travelerRepository;

        public TravelerController(ITravelerRepository travelerRepository)
        {
            _travelerRepository = travelerRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var travelers = await _travelerRepository.GetAllTravelers();
            return View(travelers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Firstname,Lastname,Email,BirthDate")] Traveler traveler)
        {
            if (ModelState.IsValid)
            {
                await _travelerRepository.AddTraveler(traveler);
                return RedirectToAction(nameof(Index));
            }
            return View(traveler);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var traveler = await _travelerRepository.GetTravelerById(id);
            if (traveler == null)
            {
                return NotFound();
            }
            return View(traveler);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TravelerId,Firstname,Lastname,Email,BirthDate")] Traveler traveler)
        {
            if (id != traveler.Id) 
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _travelerRepository.UpdateTraveler(traveler);
                return RedirectToAction(nameof(Index));
            }
            return View(traveler);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var traveler = await _travelerRepository.GetTravelerById(id);
            if (traveler == null)
            {
                return NotFound();
            }
            return View(traveler);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _travelerRepository.DeleteTraveler(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
