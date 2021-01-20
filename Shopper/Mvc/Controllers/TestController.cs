using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shopper.Database;
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
        public IActionResult Test(string host)
        {
            return Ok(Request.Protocol);
        }
    }
}
