using System.Threading.Tasks;
using EvaLabs.Domain.Entities;
using EvaLabs.Services.ExtensionMethod;
using EvaLabs.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EvaLabs.Controllers
{
    /// <summary>
    /// Areas Controller
    /// </summary>
    public class AreasController : BaseController
    {
        private readonly IAreaService _areaService;
        private readonly ICityService _cityService;

        public AreasController(IAreaService areaService, ICityService cityService)
        {
            _areaService = areaService;
            _cityService = cityService;
        }


        public async Task<IActionResult> Index()
        {
            var result = await _areaService.ListAllAsync();
            if (result.IsSucceeded)
            {
                return View(result.Data);
            }

            return Error(result.Messages);
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return BadRequest();

            var result = await _areaService.GetByIdAsync(id.Value);
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
        public async Task<IActionResult> Create(Area area)
        {
            if (ModelState.IsValid)
            {
                var result = await _areaService.CreateOrUpdateAsync(area);
                if (result.IsSucceeded)
                {
                    return RedirectToAction(nameof(Index));
                }

                return Error(result.Messages);
            }

            GetViewData(area);
            return View(area);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return BadRequest();

            var result = await _areaService.GetByIdAsync(id.Value);
            if (result.IsSucceeded)
            {
                GetViewData(result.Data);
                return View(result.Data);
            }

            return Error(result.Messages);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Area area)
        {
            if (id != area.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var result = await _areaService.CreateOrUpdateAsync(area);
                if (result.IsSucceeded)
                {
                    return RedirectToAction(nameof(Index));
                }

                return Error(result.Messages);
            }

            GetViewData(area);
            return View(area);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();

            var result = await _areaService.GetByIdAsync(id.Value);
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
            var result = await _areaService.DeleteAsync(id);
            if (result.IsSucceeded)
            {
                return RedirectToAction(nameof(Index));
            }

            return Error(result.Messages);
        }

        private void GetViewData(Area area)
        {
            ViewData["CityId"] = _cityService.AsEnumerable().AsSelectList("Id", "CityName", area?.CityId);
        }
    }
}