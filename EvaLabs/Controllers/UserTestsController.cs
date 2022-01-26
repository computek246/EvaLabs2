using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EvaLabs.Domain.Context;
using EvaLabs.Domain.Entities;

namespace EvaLabs.Controllers
{
    public class UserTestsController : BaseController
    {
        private readonly IEvaContext _context;

        public UserTestsController(IEvaContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Index()
        {
            var evaContext = _context.UserTests.Include(u => u.Area).Include(u => u.Branch).Include(u => u.City).Include(u => u.Lab).Include(u => u.Test).Include(u => u.TestStatus).Include(u => u.User);
            return View(await evaContext.ToListAsync());
        }

        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userTest = await _context.UserTests
                .Include(u => u.Area)
                .Include(u => u.Branch)
                .Include(u => u.City)
                .Include(u => u.Lab)
                .Include(u => u.Test)
                .Include(u => u.TestStatus)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userTest == null)
            {
                return NotFound();
            }

            return View(userTest);
        }

        
        public IActionResult Create()
        {
            ViewData["AreaId"] = new SelectList(_context.Areas, "Id", "Id");
            ViewData["BranchId"] = new SelectList(_context.Branches, "Id", "Id");
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Id");
            ViewData["LabId"] = new SelectList(_context.Labs, "Id", "Id");
            ViewData["TestId"] = new SelectList(_context.Tests, "Id", "Id");
            ViewData["TestStatusId"] = new SelectList(_context.TestStatus, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        
        
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserTest userTest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userTest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AreaId"] = new SelectList(_context.Areas, "Id", "Id", userTest.AreaId);
            ViewData["BranchId"] = new SelectList(_context.Branches, "Id", "Id", userTest.BranchId);
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Id", userTest.CityId);
            ViewData["LabId"] = new SelectList(_context.Labs, "Id", "Id", userTest.LabId);
            ViewData["TestId"] = new SelectList(_context.Tests, "Id", "Id", userTest.TestId);
            ViewData["TestStatusId"] = new SelectList(_context.TestStatus, "Id", "Id", userTest.TestStatusId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userTest.UserId);
            return View(userTest);
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userTest = await _context.UserTests.FindAsync(id);
            if (userTest == null)
            {
                return NotFound();
            }
            ViewData["AreaId"] = new SelectList(_context.Areas, "Id", "Id", userTest.AreaId);
            ViewData["BranchId"] = new SelectList(_context.Branches, "Id", "Id", userTest.BranchId);
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Id", userTest.CityId);
            ViewData["LabId"] = new SelectList(_context.Labs, "Id", "Id", userTest.LabId);
            ViewData["TestId"] = new SelectList(_context.Tests, "Id", "Id", userTest.TestId);
            ViewData["TestStatusId"] = new SelectList(_context.TestStatus, "Id", "Id", userTest.TestStatusId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userTest.UserId);
            return View(userTest);
        }

        
        
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UserTest userTest)
        {
            if (id != userTest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userTest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserTestExists(userTest.Id))
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
            ViewData["AreaId"] = new SelectList(_context.Areas, "Id", "Id", userTest.AreaId);
            ViewData["BranchId"] = new SelectList(_context.Branches, "Id", "Id", userTest.BranchId);
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Id", userTest.CityId);
            ViewData["LabId"] = new SelectList(_context.Labs, "Id", "Id", userTest.LabId);
            ViewData["TestId"] = new SelectList(_context.Tests, "Id", "Id", userTest.TestId);
            ViewData["TestStatusId"] = new SelectList(_context.TestStatus, "Id", "Id", userTest.TestStatusId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userTest.UserId);
            return View(userTest);
        }

        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userTest = await _context.UserTests
                .Include(u => u.Area)
                .Include(u => u.Branch)
                .Include(u => u.City)
                .Include(u => u.Lab)
                .Include(u => u.Test)
                .Include(u => u.TestStatus)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userTest == null)
            {
                return NotFound();
            }

            return View(userTest);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userTest = await _context.UserTests.FindAsync(id);
            _context.UserTests.Remove(userTest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserTestExists(int id)
        {
            return _context.UserTests.Any(e => e.Id == id);
        }
    }
}
