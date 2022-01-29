using System.Threading.Tasks;
using EvaLabs.Domain.Entities;
using EvaLabs.Services.ExtensionMethod;
using EvaLabs.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EvaLabs.Controllers
{
    public class TestBranchsController : BaseController
    {
        private readonly ITestBranchsService _testBranchsService;
        private readonly ITestService _testService;
        private readonly IBranchService _branchService;

        public TestBranchsController(
            ITestBranchsService testBranchsService,
            ITestService testService,
            IBranchService branchService)
        {
            _testBranchsService = testBranchsService;
            _testService = testService;
            _branchService = branchService;
        }


        public async Task<IActionResult> Index()
        {
            var result = await _testBranchsService.ListAllAsync();
            if (result.IsSucceeded)
            {
                return View(result.Data);
            }

            return Error(result.Messages);
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return BadRequest();

            var result = await _testBranchsService.GetByIdAsync(id.Value);
            if (result.IsSucceeded)
            {
                return View(result.Data);
            }

            return Error(result.Messages);
        }


        public IActionResult Create()
        {
            GetViewData(null);
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TestBranch testBranch)
        {
            if (ModelState.IsValid)
            {
                var result = await _testBranchsService.CreateOrUpdateAsync(testBranch);
                if (result.IsSucceeded)
                {
                    return RedirectToAction(nameof(Index));
                }

                return Error(result.Messages);
            }

            GetViewData(testBranch);
            return View(testBranch);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return BadRequest();

            var result = await _testBranchsService.GetByIdAsync(id.Value);
            if (result.IsSucceeded)
            {
                GetViewData(result.Data);
                return View(result.Data);
            }

            return Error(result.Messages);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TestBranch testBranch)
        {
            if (id != testBranch.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var result = await _testBranchsService.CreateOrUpdateAsync(testBranch);
                if (result.IsSucceeded)
                {
                    return RedirectToAction(nameof(Index));
                }

                return Error(result.Messages);
            }

            GetViewData(testBranch);
            return View(testBranch);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();

            var result = await _testBranchsService.GetByIdAsync(id.Value);
            if (result.IsSucceeded)
            {
                return View(result.Data);
            }

            return Error(result.Messages);
        }


        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _testBranchsService.DeleteAsync(id);
            if (result.IsSucceeded)
            {
                return RedirectToAction(nameof(Index));
            }

            return Error(result.Messages);
        }

        private void GetViewData(TestBranch testBranch)
        {
            ViewData["BranchId"] = _branchService.AsEnumerable().AsSelectList("Id", "BranchName", testBranch?.BranchId);
            ViewData["TestId"] = _testService.AsEnumerable().AsSelectList("Id", "TestName", testBranch?.TestId);
        }
    }
}