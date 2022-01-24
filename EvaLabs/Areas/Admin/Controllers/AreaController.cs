using System.Linq;
using System.Threading.Tasks;
using EvaLabs.Domain.Context;
using EvaLabs.Domain.Entities;
using EvaLabs.Services.ExtensionMethod;
using EvaLabs.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EvaLabs.Areas.Admin.Controllers
{
    public class AreaController : AdminBaseController
    {
        private readonly ICityService _cityService;
        private readonly EvaContext _context;

        public AreaController(ICityService cityService, EvaContext context)
        {
            _cityService = cityService;
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            var evaContext = _context.Areas.Include(a => a.City);
            return View(await evaContext.ToListAsync());
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var area = await _context.Areas
                .Include(a => a.City)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (area == null) return NotFound();

            return View(area);
        }


        public IActionResult Create()
        {
            GetViewData(null);
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Area area)
        {
            if (ModelState.IsValid)
            {
                _context.Add(area);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            GetViewData(area);
            return View(area);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var area = await _context.Areas.FindAsync(id);
            if (area == null) return NotFound();
            GetViewData(area);
            return View(area);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Area area)
        {
            if (id != area.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(area);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AreaExists(area.Id))
                        return NotFound();
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            GetViewData(area);
            return View(area);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var area = await _context.Areas
                .Include(a => a.City)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (area == null) return NotFound();

            return View(area);
        }


        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var area = await _context.Areas.FindAsync(id);
            _context.Areas.Remove(area);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AreaExists(int id)
        {
            return _context.Areas.Any(e => e.Id == id);
        }

        private void GetViewData(Area area)
        {
            ViewData["CityId"] = _cityService.AsEnumerable().AsSelectList("Id", "CityName", area?.CityId);
        }
    }
}