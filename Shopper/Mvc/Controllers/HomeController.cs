﻿using System;
 using System.Diagnostics;
 using System.Linq;
 using System.Threading.Tasks;
 using Microsoft.AspNetCore.Authorization;
 using Microsoft.AspNetCore.Mvc;
 using Microsoft.EntityFrameworkCore;
 using Microsoft.Extensions.DependencyInjection;
 using Serilog;
 using Shared.Mvc.Entities;
 using Shared.Mvc.Entities.BaseEntities;
 using Shared.Mvc.ViewModels;
 using Shopper.Extensions.Helpers;
 using Shopper.Mvc.ViewModels;
 using Shopper.Services;
 using Shopper.Services.Interfaces;

 namespace Shopper.Mvc.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        private readonly ILogger _logger;
        private readonly IReportService _reportService;

        public HomeController(ILogger logger, IReportService reportService)
        {
            _logger = logger;
            _reportService = reportService;
        }

        public async Task<IActionResult> Index()
        {
            // return Ok(DateTime.Now.DateWithSuffix());
            var sales = await _reportService.GetThisYearSalesAsync();
            var expenditures = await _reportService.GetThisYearExpenditure();
            var dashboardModal = new DashboardModel
            {
                Sales = sales,
                Expenditures = expenditures,
                Summaries = _reportService.GetSummariesAsync(sales, expenditures)
            };

            AddPageHeader("Dashboard");

            // return Ok(dashboardModal.Sales.ToArray());
            return View(dashboardModal);
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
