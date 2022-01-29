using System.Threading.Tasks;
using EvaLabs.Domain.Entities;
using EvaLabs.Services.ExtensionMethod;
using EvaLabs.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EvaLabs.Controllers
{
    /// <summary>
    /// Branches Controller
    /// </summary>
    public class BranchesController : BaseController
    {
        private readonly IBranchService _branchService;
        private readonly ILabService _labService;
        private readonly IAreaService _areaService;

        public BranchesController(
            IBranchService branchService,
            ILabService labService,
            IAreaService areaService
            )
        {
            _branchService = branchService;
            _labService = labService;
            _areaService = areaService;
        }


        public async Task<IActionResult> Index()
        {
            var result = await _branchService.ListAllAsync();
            if (result.IsSucceeded)
            {
                return View(result.Data);
            }

            return Error(result.Messages);
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return BadRequest();

            var result = await _branchService.GetByIdAsync(id.Value);
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
        public async Task<IActionResult> Create(Branch branch)
        {
            if (ModelState.IsValid)
            {
                var result = await _branchService.CreateOrUpdateAsync(branch);
                if (result.IsSucceeded)
                {
                    return RedirectToAction(nameof(Index));
                }

                return Error(result.Messages);
            }

            GetViewData(branch);
            return View(branch);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return BadRequest();

            var result = await _branchService.GetByIdAsync(id.Value);
            if (result.IsSucceeded)
            {
                GetViewData(result.Data);
                return View(result.Data);
            }

            return Error(result.Messages);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Branch branch)
        {
            if (id != branch.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var result = await _branchService.CreateOrUpdateAsync(branch);
                if (result.IsSucceeded)
                {
                    return RedirectToAction(nameof(Index));
                }

                return Error(result.Messages);
            }

            GetViewData(branch);
            return View(branch);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();

            var result = await _branchService.GetByIdAsync(id.Value);
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
            var result = await _branchService.DeleteAsync(id);
            if (result.IsSucceeded)
            {
                return RedirectToAction(nameof(Index));
            }

            return Error(result.Messages);
        }



        private void GetViewData(Branch branch)
        {
            ViewData["AreaId"] = _areaService.AsEnumerable().AsSelectList("Id", "AreaName", branch?.AreaId);
            ViewData["LabId"] = _labService.AsEnumerable().AsSelectList("Id", "LabName", branch?.LabId);
        }
    }
}