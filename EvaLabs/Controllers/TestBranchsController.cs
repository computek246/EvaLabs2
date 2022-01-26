using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EvaLabs.Domain.Context;
using EvaLabs.Domain.Entities;

namespace EvaLabs.Controllers
{
    public class TestBranchsController : BaseController
    {
        private readonly IEvaContext _context;

        public TestBranchsController(IEvaContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Index()
        {
            var evaContext = _context.TestBranches.Include(t => t.Branch).Include(t => t.Test);
            return View(await evaContext.ToListAsync());
        }

        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testBranch = await _context.TestBranches
                .Include(t => t.Branch)
                .Include(t => t.Test)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (testBranch == null)
            {
                return NotFound();
            }

            return View(testBranch);
        }

        
        public IActionResult Create()
        {
            ViewData["BranchId"] = new SelectList(_context.Branches, "Id", "Id");
            ViewData["TestId"] = new SelectList(_context.Tests, "Id", "Id");
            return View();
        }

        
        
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TestBranch testBranch)
        {
            if (ModelState.IsValid)
            {
                _context.Add(testBranch);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BranchId"] = new SelectList(_context.Branches, "Id", "Id", testBranch.BranchId);
            ViewData["TestId"] = new SelectList(_context.Tests, "Id", "Id", testBranch.TestId);
            return View(testBranch);
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testBranch = await _context.TestBranches.FindAsync(id);
            if (testBranch == null)
            {
                return NotFound();
            }
            ViewData["BranchId"] = new SelectList(_context.Branches, "Id", "Id", testBranch.BranchId);
            ViewData["TestId"] = new SelectList(_context.Tests, "Id", "Id", testBranch.TestId);
            return View(testBranch);
        }

        
        
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TestBranch testBranch)
        {
            if (id != testBranch.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(testBranch);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestBranchExists(testBranch.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BranchId"] = new SelectList(_context.Branches, "Id", "Id", testBranch.BranchId);
            ViewData["TestId"] = new SelectList(_context.Tests, "Id", "Id", testBranch.TestId);
            return View(testBranch);
        }

        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testBranch = await _context.TestBranches
                .Include(t => t.Branch)
                .Include(t => t.Test)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (testBranch == null)
            {
                return NotFound();
            }

            return View(testBranch);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var testBranch = await _context.TestBranches.FindAsync(id);
            _context.TestBranches.Remove(testBranch);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TestBranchExists(int id)
        {
            return _context.TestBranches.Any(e => e.Id == id);
        }
    }
}
