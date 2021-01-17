﻿using System;
 using System.Diagnostics;
 using System.Threading.Tasks;
 using Microsoft.AspNetCore.Authorization;
 using Microsoft.AspNetCore.Mvc;
 using Serilog;
 using Shared.Mvc.ViewModels;
 using Shopper.Mvc.ViewModels;
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
            var sales = await _reportService.GetThisYearSalesAsync();
            var stockExpiry = await _reportService.GetThisYearStockExpiryCountAsync();
            // return Ok(sales.Where(s => s.Sku.ProductId == 2).Select(s => s.Sku.Sales).ToList());
            var expenditures = await _reportService.GetThisYearExpenditure();
            var dashboardModal = new DashboardModel
            {
                StockExpiryModel = stockExpiry,
                Sales = sales,
                Expenditures = expenditures,
                Summaries = _reportService.GetSummariesAsync(sales, expenditures)
            };

            AddPageHeader($"Dashboard Summary ({DateTime.Now.AddMonths(-11).ToString("MMM yyyy")} - {DateTime.Now.ToString("MMM yyyy")})");

            // return Ok(dashboardModal.Sales.ToArray());
            return View(dashboardModal);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}
