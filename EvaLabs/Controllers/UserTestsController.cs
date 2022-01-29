using System.Threading.Tasks;
using EvaLabs.Domain.Entities;
using EvaLabs.Services.ExtensionMethod;
using EvaLabs.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EvaLabs.Controllers
{
    /// <summary>
    /// UserTests Controller
    /// </summary>
    public class UserTestsController : BaseController
    {
        private readonly IUserTestService _userTestService;
        private readonly IAreaService _areaService;
        private readonly IBranchService _branchService;
        private readonly ICityService _cityService;
        private readonly ILabService _labService;
        private readonly ITestService _testService;

        public UserTestsController(
            IUserTestService userTestService,
            IAreaService areaService,
            IBranchService branchService,
            ICityService cityService,
            ILabService labService,
            ITestService testService
            )
        {
            _userTestService = userTestService;
            _areaService = areaService;
            _branchService = branchService;
            _cityService = cityService;
            _labService = labService;
            _testService = testService;
        }


        public async Task<IActionResult> Index()
        {
            var result = await _userTestService.ListAllAsync();
            if (result.IsSucceeded)
            {
                return View(result.Data);
            }

            return Error(result.Messages);
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return BadRequest();

            var result = await _userTestService.GetByIdAsync(id.Value);
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
        public async Task<IActionResult> Create(UserTest userTest)
        {
            if (ModelState.IsValid)
            {
                var result = await _userTestService.CreateOrUpdateAsync(userTest);
                if (result.IsSucceeded)
                {
                    return RedirectToAction(nameof(Index));
                }

                return Error(result.Messages);
            }

            GetViewData(userTest);
            return View(userTest);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return BadRequest();

            var result = await _userTestService.GetByIdAsync(id.Value);
            if (result.IsSucceeded)
            {
                GetViewData(result.Data);
                return View(result.Data);
            }

            return Error(result.Messages);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UserTest userTest)
        {
            if (id != userTest.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var result = await _userTestService.CreateOrUpdateAsync(userTest);
                if (result.IsSucceeded)
                {
                    return RedirectToAction(nameof(Index));
                }

                return Error(result.Messages);
            }

            GetViewData(userTest);
            return View(userTest);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();

            var result = await _userTestService.GetByIdAsync(id.Value);
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
            var result = await _userTestService.DeleteAsync(id);
            if (result.IsSucceeded)
            {
                return RedirectToAction(nameof(Index));
            }

            return Error(result.Messages);
        }

        private void GetViewData(UserTest userTest)
        {
            ViewData["AreaId"] = _areaService.AsEnumerable().AsSelectList("Id", "AreaName", userTest?.AreaId);
            ViewData["BranchId"] = _branchService.AsEnumerable().AsSelectList("Id", "BranchName", userTest?.BranchId);
            ViewData["CityId"] = _cityService.AsEnumerable().AsSelectList("Id", "CityName", userTest?.CityId);
            ViewData["LabId"] = _labService.AsEnumerable().AsSelectList("Id", "LabName", userTest?.LabId);
            ViewData["TestId"] = _testService.AsEnumerable().AsSelectList("Id", "TestName", userTest?.TestId);
        }
    }
}