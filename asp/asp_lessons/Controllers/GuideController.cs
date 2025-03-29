using Microsoft.AspNetCore.Mvc;
using aspapp.Data.Models;
using aspapp.Data.Repositories;
using System.Threading.Tasks;

namespace aspapp.Controllers
{
    public class GuideController : Controller
    {
        private readonly IGuideRepository _guideRepository;

        public GuideController(IGuideRepository guideRepository)
        {
            _guideRepository = guideRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var guides = await _guideRepository.GetAllGuides();
            return View(guides);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Firstname,Lastname,Email,BirthDate")] Guide guide)
        {
            if (ModelState.IsValid)
            {
                await _guideRepository.AddGuide(guide);
                return RedirectToAction(nameof(Index));
            }
            return View(guide);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var guide = await _guideRepository.GetGuideById(id);
            if (guide == null)
            {
                return NotFound();
            }
            return View(guide);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GuideId,Firstname,Lastname,Email,BirthDate")] Guide guide)
        {
            if (id != guide.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _guideRepository.UpdateGuide(guide);
                return RedirectToAction(nameof(Index));
            }
            return View(guide);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var guide = await _guideRepository.GetGuideById(id);
            if (guide == null)
            {
                return NotFound();
            }
            return View(guide);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _guideRepository.DeleteGuide(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
