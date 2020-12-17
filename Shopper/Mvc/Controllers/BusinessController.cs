using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shopper.Attributes;
using Shopper.Extensions.Helpers;
using Shopper.Mvc.ViewModels;
using Shopper.Services.Interfaces;

namespace Shopper.Mvc.Controllers
{
    [Microsoft.AspNetCore.Components.Route("business")]
    public class BusinessController : BaseController
    {
        private readonly IBusinessService _businessService;

        public BusinessController(IBusinessService businessService)
        {
            _businessService = businessService;
        }

        [HttpGet("info")]
        public IActionResult Show()
        {
            var tenant = HttpContext.GetCurrentTenant();
            var viewModel = new BusinessInfoModel
            {
                Id = tenant.Id,
                Name = tenant.Name,
                Email = tenant.Email,
                Address = tenant.Address,
                Phone1 = tenant.PhoneNumber1,
                Phone2 = tenant.PhoneNumber2,
                Description = tenant.Description,
                ValidTo = tenant.ValidTo,
                SubscriptionType = tenant.SubscriptionType.ToString(),
                LogoPath = tenant.LogoPath.LoadTenantImage(HttpContext)
            };
            return View(viewModel);
        }

        [HttpPost("update-info"), Permission("business_info_update"), ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateBusinessInfo(BusinessInfoModel formModel)
        {
            await _businessService.UpdateBusinessInfo(formModel, HttpContext.GetCurrentTenant());

            return RedirectToAction("Show");
        }

        [AcceptVerbs("GET", Route = "validate-tenant-name", Name = "ValidateTenantName")]
        public async Task<IActionResult> ExistsByDisplayName(string name, Guid id)
        {
            return await _businessService.IsDuplicateAsync(name, id)
                ? Json("Another business with this name with this name already exists")
                : Json(true);
        }
    }
}
