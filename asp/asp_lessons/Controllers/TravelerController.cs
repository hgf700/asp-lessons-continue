﻿using Microsoft.AspNetCore.Mvc;
using aspapp.Models;
using aspapp.Services;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using aspapp.Models.VM;

namespace aspapp.Controllers
{
    [Route("traveler")]
    public class TravelerController : Controller
    {
        private readonly ITravelerService _travelerService;
        private readonly UserManager<IdentityUser> _userManager;
        string[] roleNames = { "Admin", "Guide", "User" };

        public TravelerController(ITravelerService travelerService, UserManager<IdentityUser> userManager)
        {
            _travelerService = travelerService;
            _userManager = userManager;
        }

        // Index GET
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var travelers = await _travelerService.GetAllTravelers();
            return View(travelers);
        }

        [AllowAnonymous]
        [HttpGet("create")]
        public IActionResult Create() => View();

        [AllowAnonymous]
        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Firstname,Lastname,Email,BirthDate")] TravelerViewModel traveler)
        {
            if (ModelState.IsValid)
            {
                var passworddef = "asd123A!";
                var identityUser = new IdentityUser { UserName = traveler.Email, Email = traveler.Email, PasswordHash=passworddef };
                var result = await _userManager.CreateAsync(identityUser);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(identityUser, roleNames[2]);

                    await _travelerService.AddTraveler(traveler);
                    return RedirectToAction(nameof(Index));
                }

                await _travelerService.AddTraveler(traveler);
                return RedirectToAction(nameof(Index));
            }
            return View(traveler);
        }

        [Authorize(Roles = "Admin,User")]
        //[AllowAnonymous]
        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var traveler = await _travelerService.GetTravelerById(id);
            if (traveler == null) return NotFound();
            return View(traveler);
        }

        [Authorize(Roles = "Admin,User")]
        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TravelerId,Firstname,Lastname,Email,BirthDate")] TravelerViewModel traveler)
        {

            if (ModelState.IsValid)
            {
                await _travelerService.UpdateTraveler(traveler);
                return RedirectToAction(nameof(Index));
            }
            return View(traveler);
        }

        // Delete GET
        [Authorize(Roles = "Admin")]
        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var traveler = await _travelerService.GetTravelerById(id);
            if (traveler == null) return NotFound();
            return View(traveler);
        }

        // Delete POST
        [Authorize(Roles = "Admin")]
        [HttpPost("delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _travelerService.DeleteTraveler(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
