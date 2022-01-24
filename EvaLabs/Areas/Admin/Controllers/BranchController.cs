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
    public class BranchController : AdminBaseController
    {
        private readonly IAreaService _areaService;
        private readonly ILabService _labService;
        private readonly EvaContext _context;

        public BranchController(IAreaService areaService, ILabService labService, EvaContext context)
        {
            _areaService = areaService;
            _labService = labService;
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            var evaContext = _context.Branches.Include(b => b.Area).Include(b => b.Lab);
            return View(await evaContext.ToListAsync());
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var branch = await _context.Branches
                .Include(b => b.Area)
                .Include(b => b.Lab)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (branch == null) return NotFound();

            return View(branch);
        }


        public IActionResult Create()
        {
            GetViewData(null);
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Branch branch)
        {
            if (ModelState.IsValid)
            {
                _context.Add(branch);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            GetViewData(branch);
            return View(branch);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var branch = await _context.Branches.FindAsync(id);
            if (branch == null) return NotFound();
            GetViewData(branch);
            return View(branch);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Branch branch)
        {
            if (id != branch.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(branch);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BranchExists(branch.Id))
                        return NotFound();
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            GetViewData(branch);
            return View(branch);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var branch = await _context.Branches
                .Include(b => b.Area)
                .Include(b => b.Lab)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (branch == null) return NotFound();

            return View(branch);
        }


        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var branch = await _context.Branches.FindAsync(id);
            _context.Branches.Remove(branch);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BranchExists(int id)
        {
            return _context.Branches.Any(e => e.Id == id);
        }

        private void GetViewData(Branch branch)
        {
            ViewData["AreaId"] = _areaService.AsEnumerable().AsSelectList("Id", "AreaName", "City.CityName", branch?.AreaId);
            ViewData["LabId"] = _labService.AsEnumerable().AsSelectList("Id", "LabName", branch?.LabId);
        }
    }
}