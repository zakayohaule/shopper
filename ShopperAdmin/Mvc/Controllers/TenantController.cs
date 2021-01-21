using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopperAdmin.Services.Interfaces;

namespace ShopperAdmin.Mvc.Controllers
{
    [Authorize, Route("tenants")]
    public class TenantController : BaseController
    {
        private readonly ITenantService _tenantService;

        public TenantController(ITenantService tenantService)
        {
            _tenantService = tenantService;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var tenants = await _tenantService.GetTenantAsQuerable()
                .AsNoTracking()
                .ToListAsync();

            return View(tenants);
        }
    }
}
