﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using AutoMapper;
using EvaLabs.Domain.Entities;
using EvaLabs.Infrastructure;
using EvaLabs.Models;
using EvaLabs.ViewModels;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EvaLabs.Controllers
{

    public class HomeController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public HomeController(
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
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





    }
}