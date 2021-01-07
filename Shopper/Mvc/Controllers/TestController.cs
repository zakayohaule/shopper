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
        public async Task<IActionResult> Test(string host)
        {
            var subDomain = string.Empty;
            if (host.Contains("."))
            {
                var domainParts = host.Split(".");
                if (domainParts.Length == 2 || domainParts.Length == 3)
                {
                    subDomain = domainParts[0];
                }
                else
                {
                    subDomain = domainParts[1];
                }
            }

            return Ok(subDomain);
        }
    }
}
