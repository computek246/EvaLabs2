using System.Threading.Tasks;
using EvaLabs.Common.ExtensionMethod;
using EvaLabs.Domain.Entities;
using EvaLabs.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EvaLabs.Controllers
{
    /// <summary>
    /// Cities Controller
    /// </summary>
    public class CitiesController : BaseController
    {
        private readonly ICityService _cityService;

        public CitiesController(ICityService cityService)
        {
            _cityService = cityService;
        }


        public async Task<IActionResult> Index()
        {
            var result = await _cityService.ListAllAsync();
            if (result.IsSucceeded)
            {
                return View(result.Data);
            }

            return Error(result.Messages);
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return BadRequest();

            var result = await _cityService.GetByIdAsync(id.Value);
            if (result.IsSucceeded)
            {
                return View(result.Data);
            }

            return Error(result.Messages);
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(City city)
        {
            if (ModelState.IsValid)
            {
                var result = await _cityService.CreateOrUpdateAsync(city);
                if (result.IsSucceeded)
                {
                    return RedirectToAction(nameof(Index));
                }

                return Error(result.Messages);
            }

            return View(city);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return BadRequest();

            var result = await _cityService.GetByIdAsync(id.Value);
            if (result.IsSucceeded)
            {
                return View(result.Data);
            }

            return Error(result.Messages);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, City city)
        {
            if (id != city.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var result = await _cityService.CreateOrUpdateAsync(city);
                if (result.IsSucceeded)
                {
                    return RedirectToAction(nameof(Index));
                }

                return Error(result.Messages);
            }

            return View(city);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();

            var result = await _cityService.GetByIdAsync(id.Value);
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
            var result = await _cityService.DeleteAsync(id);
            if (result.IsSucceeded)
            {
                return RedirectToAction(nameof(Index));
            }

            return Error(result.Messages);
        }
    }
}