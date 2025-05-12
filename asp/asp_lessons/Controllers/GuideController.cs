using Microsoft.AspNetCore.Mvc;
using aspapp.Models;
using aspapp.Services;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using aspapp.Models.VM;
using Microsoft.AspNetCore.Authorization;

namespace aspapp.Controllers
{
    [Authorize(Roles = "Guide,Administrator")]
    [Route("guide")]
    public class GuideController : Controller
    {
        private readonly IGuideService _guideService;

        public GuideController(IGuideService guideService)
        {
            _guideService = guideService;
        }

        // Index GET
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var guides = await _guideService.GetAllGuides();
            return View(guides);
        }

        // Create GET
        [HttpGet("create")]
        public IActionResult Create() => View();

        // Create POST
        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Firstname,Lastname,Email,BirthDate")] GuideViewModel guideViewModel)
        {
            if (ModelState.IsValid)
            {
                await _guideService.AddGuide(guideViewModel); // Assuming this method takes GuideViewModel
                return RedirectToAction(nameof(Index));
            }
            return View(guideViewModel);
        }
    }
}
