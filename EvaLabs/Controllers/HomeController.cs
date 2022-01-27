using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using EvaLabs.Domain.Context;
using EvaLabs.Domain.Entities;
using EvaLabs.Infrastructure;
using EvaLabs.Models;
using EvaLabs.ViewModels;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EvaLabs.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IEvaContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(
            IEvaContext context,
            IUnitOfWork unitOfWork,
            IMapper mapper
        )
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var apiHelpViewModel =
                Assembly.GetExecutingAssembly().GetAssemblyMethodInfo<BaseController>()
                    .Where(x => x.Name == nameof(Index))
                    .Select(x => x.ToMethodInfo())
                    .OrderBy(x => x.Area)
                    .ThenBy(x => x.Controller)
                    .ThenBy(x => x.Action)
                    .ToList();
            return View(apiHelpViewModel);
        }

        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }


        [AllowAnonymous]
        public IActionResult About()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Branches()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Tests()
        {
            return View();
        }

        public IActionResult Reservations()
        {
            return View();
        }

        public IActionResult CreateReservation()
        {
            ViewData["TestId"] = new SelectList(_context.Tests, "Id", "TestName");
            return View();
        }


        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [HttpPost]
        public ActionResult Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);
            return File(fileContents, contentType, fileName);
        }


        [AllowAnonymous]
        public async Task<IActionResult> Branches_Read([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                var result = await _unitOfWork.GetRepository<Branch>()
                    .GetAll(t => t.IsActive)
                    .Include(e => e.Area)
                    .ThenInclude(e => e.City)
                    .ToListAsync();
                var list = _mapper.Map<List<BranchViewModel>>(result);
                return Json(await list.ToDataSourceResultAsync(request));
            }
            catch (Exception exception)
            {
                return Json(exception.Message);
            }
        }


        [AllowAnonymous]
        public async Task<IActionResult> Tests_Read([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                var result = await _unitOfWork.GetRepository<Test>()
                    .GetAll(t => t.IsActive)
                    .ToListAsync();
                var list = _mapper.Map<List<TestViewModel>>(result);
                return Json(await list.ToDataSourceResultAsync(request));
            }
            catch (Exception exception)
            {
                return Json(exception.Message);
            }
        }


        public async Task<IActionResult> GeBranches()
        {
            return Json(await _unitOfWork.GetRepository<Branch>()
                .GetAll(t => t.IsActive).Select(x => new
                {
                    Id = x.Id,
                    Name = x.BranchName
                }).ToListAsync());
        }

        public async Task<IActionResult> GetCities()
        {
            return Json(await _unitOfWork.GetRepository<City>()
                .GetAll(t => t.IsActive).Select(x => new
                {
                    Id = x.Id,
                    Name = x.CityName
                }).ToListAsync());
        }

        public async Task<IActionResult> GetAreas(int cityId)
        {
            return Json(await _unitOfWork.GetRepository<Area>()
                .GetAll(t => t.IsActive && t.CityId == cityId).Select(x => new
                {
                    Id = x.Id,
                    Name = x.AreaName
                }).ToListAsync());
        }
    }
}