using System.Threading.Tasks;
using EvaLabs.Domain.Entities;
using EvaLabs.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EvaLabs.Controllers
{
    /// <summary>
    /// Labs Controller
    /// </summary>
    public class LabsController : BaseController
    {
        private readonly ILabService _labService;

        public LabsController(ILabService labService)
        {
            _labService = labService;
        }


        public async Task<IActionResult> Index()
        {
            var result = await _labService.ListAllAsync();
            if (result.IsSucceeded)
            {
                return View(result.Data);
            }

            return Error(result.Messages);
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return BadRequest();

            var result = await _labService.GetByIdAsync(id.Value);
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
        public async Task<IActionResult> Create(Lab lab)
        {
            if (ModelState.IsValid)
            {
                var result = await _labService.CreateOrUpdateAsync(lab);
                if (result.IsSucceeded)
                {
                    return RedirectToAction(nameof(Index));
                }

                return Error(result.Messages);
            }

            return View(lab);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return BadRequest();

            var result = await _labService.GetByIdAsync(id.Value);
            if (result.IsSucceeded)
            {
                return View(result.Data);
            }

            return Error(result.Messages);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Lab lab)
        {
            if (id != lab.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var result = await _labService.CreateOrUpdateAsync(lab);
                if (result.IsSucceeded)
                {
                    return RedirectToAction(nameof(Index));
                }

                return Error(result.Messages);
            }

            return View(lab);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();

            var result = await _labService.GetByIdAsync(id.Value);
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
            var result = await _labService.DeleteAsync(id);
            if (result.IsSucceeded)
            {
                return RedirectToAction(nameof(Index));
            }

            return Error(result.Messages);
        }
    }
}