﻿using System.Diagnostics;
 using Microsoft.AspNetCore.Authorization;
 using Microsoft.AspNetCore.Mvc;
 using Microsoft.Extensions.DependencyInjection;
 using Serilog;
 using Shared.Mvc.Entities;
 using Shared.Mvc.Entities.BaseEntities;
 using Shared.Mvc.ViewModels;
 using Shopper.Services;

 namespace Shopper.Mvc.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        private readonly ILogger _logger;
        public HomeController(ILogger logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            AddPageHeader("Dashboard");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}
