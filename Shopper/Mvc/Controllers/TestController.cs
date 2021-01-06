using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopper.Database;
using Shopper.Extensions.Helpers;
using Shopper.Services.Interfaces;

namespace Shopper.Mvc.Controllers
{
    public class TestController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ITenantService _tenantService;
        private readonly ISaleService _saleService;
        public TestController(ApplicationDbContext dbContext, ITenantService tenantService, ISaleService saleService)
        {
            _dbContext = dbContext;
            _tenantService = tenantService;
            _saleService = saleService;
        }

        [HttpGet("test")]
        public async Task<IActionResult> Test(int number)
        {
            return Ok(await _saleService.GenerateInvoiceNumberAsync(HttpContext.GetCurrentTenant().Code));
        }
    }
}
