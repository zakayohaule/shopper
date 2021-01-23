using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopperAdmin.Attributes;
using ShopperAdmin.Mvc.ViewModels;
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

        [HttpGet(""), Permission("tenant_view")]
        public async Task<IActionResult> Index()
        {
            var tenants = await _tenantService.GetTenantAsQueryable()
                .AsNoTracking()
                .ToListAsync();

            return View(tenants);
        }

        [HttpGet("create"), Permission("tenant_add")]
        public ViewResult Create()
        {
            AddPageHeader("Add Tenant");
            return View();
        }

        [HttpPost("create"), ValidateAntiForgeryToken, Permission("tenant_add")]
        public async Task<IActionResult> Store(CreateTenantModel formModel)
        {
            try
            {
                await _tenantService.CreateAsync(formModel);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                ToastError(e.Message);
                return RedirectToAction("Index");
            }

            ToastSuccess("Tenant Added Successfully");
            return RedirectToAction("Index");
        }


        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var tenant = await _tenantService.FindByIdAsync(id);
            if (tenant == null)
            {
                return NotFound("A tenant could not be found");
            }

            await _tenantService.DeleteAsync(tenant);

            ToastSuccess("Tenant deleted successfully!");
            return RedirectToAction("Index");
        }

        [AcceptVerbs("GET", Route = "validate-tenant-name", Name = "ValidateTenantName")]
        public IActionResult ExistsByName(string name, ushort id)
        {
            return Json(true);
        }

        [AcceptVerbs("GET", Route = "validate-tenant-domain", Name = "ValidateTenantDomain")]
        public IActionResult ExistsByDomain(string name, ushort id)
        {
            return Json(true);
        }

        [AcceptVerbs("GET", Route = "validate-tenant-code", Name = "ValidateTenantCode")]
        public IActionResult ExistsByCode(string name, ushort id)
        {
            return Json(true);
        }

        [AcceptVerbs("GET", Route = "validate-tenant-admin-email", Name = "ValidateTenantAdminEmail")]
        public IActionResult ExistsByAdminEmail(string name, ushort id)
        {
            return Json(true);
        }

    }
}
