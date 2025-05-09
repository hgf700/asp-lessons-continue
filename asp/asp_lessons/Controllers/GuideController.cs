//using Microsoft.AspNetCore.Mvc;
//using aspapp.Models;
//using aspapp.Services;
//using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;
//using aspapp.Models.VM;

//namespace aspapp.Controllers
//{
//    [Route("guide")]
//    public class GuideController : Controller
//    {
//        private readonly IGuideService _guideService;

//        public GuideController(IGuideService guideService)
//        {
//            _guideService = guideService;
//        }

//        // Index GET
//        [HttpGet]
//        public async Task<IActionResult> Index()
//        {
//            var guides = await _guideService.GetAllGuides().ToListAsync();
//            return View(guides);
//        }

//        // Create GET
//        [HttpGet("create")]
//        public IActionResult Create() => View();

//        // Create POST
//        [HttpPost("create")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create([Bind("Firstname,Lastname,Email,BirthDate")] GuideViewModel guideViewModel)
//        {
//            if (ModelState.IsValid)
//            {
//                await _guideService.AddGuide(guideViewModel); // Assuming this method takes GuideViewModel
//                return RedirectToAction(nameof(Index));
//            }
//            return View(guideViewModel);
//        }

//        // Edit GET
//        [HttpGet("edit/{id}")]
//        public async Task<IActionResult> Edit(int id)
//        {
//            var guide = await _guideService.GetGuideById(id);
//            if (guide == null) return NotFound();
//            return View(guide); // Assuming guide is GuideViewModel or equivalent
//        }

//        // Edit POST
//        [HttpPost("edit/{id}")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, [Bind("GuideId,Firstname,Lastname,Email,BirthDate")] GuideViewModel guideViewModel)
//        {
//            if (id != guideViewModel.GuideId) return BadRequest();

//            if (ModelState.IsValid)
//            {
//                await _guideService.UpdateGuide(guideViewModel); // Assuming this method accepts GuideViewModel
//                return RedirectToAction(nameof(Index));
//            }
//            return View(guideViewModel);
//        }

//        // Delete GET
//        [HttpGet("delete/{id}")]
//        public async Task<IActionResult> Delete(int id)
//        {
//            var guide = await _guideService.GetGuideById(id);
//            if (guide == null) return NotFound();
//            return View(guide); // Assuming guide is GuideViewModel or equivalent
//        }

//        // Delete POST
//        [HttpPost("delete/{id}")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            await _guideService.DeleteGuide(id);
//            return RedirectToAction(nameof(Index));
//        }
//    }
//}
