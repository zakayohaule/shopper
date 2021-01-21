﻿using System;
 using System.Diagnostics;
 using Microsoft.AspNetCore.Authorization;
 using Microsoft.AspNetCore.Mvc;
 using Serilog;
 using ShopperAdmin.Mvc.ViewModels;

 namespace ShopperAdmin.Mvc.Controllers
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
