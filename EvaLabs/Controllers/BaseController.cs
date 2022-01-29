using System.Collections.Generic;
using EvaLabs.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EvaLabs.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {

        public IActionResult Error(List<string> messages)
        {
            return View("Error", new ErrorViewModel
            {
                Messages = messages
            });
        }
    }
}