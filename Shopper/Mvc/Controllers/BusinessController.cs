using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Shopper.Attributes;
using Shopper.Extensions.Helpers;
using Shopper.Mvc.ViewModels;
using Shopper.Services.Interfaces;

namespace Shopper.Mvc.Controllers
{
    [Route("business"), Authorize]
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

        // @todo validate all image uploads

        [AcceptVerbs("GET", Route = "validate-image-extension", Name = "ValidateImageExtension")]
        public IActionResult ValidateImageExtension(string image, [FromServices] IConfiguration configuration)
        {
            var acceptedImageFormatsKvp = configuration.GetSection("ImageFormats").AsEnumerable();
            var acceptedImageFormats = acceptedImageFormatsKvp.Where(s => s.Value!=null).Select(s => s.Value).ToList();
            var imageParts = image.Split(".");
            if (imageParts.Length != 2)
            {
                return Json("Invalid image file!");
            }

            if (!acceptedImageFormats.Contains(imageParts[1].ToLower()))
            {
                return Json($"Only '{acceptedImageFormats.Join(", ")}' image types are supported");
            }

            return Json(true);
        }
    }
}
