using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shared.Extensions.Helpers;
using Shared.Mvc.Entities;
using Shopper.Attributes;
using Shopper.Services.Interfaces;

namespace Shopper.Mvc.Controllers
{
    [Route("price-types"), Authorize]
    public class PriceTypeController : BaseController
    {
        private readonly IPriceTypeService _priceTypeService;

        public PriceTypeController(IPriceTypeService priceTypeService)
        {
            _priceTypeService = priceTypeService;
        }

        [HttpGet("", Name = "price-type-index"), Permission("price_type_view"), Toast]
        public IActionResult Index()
        {
            Title = "Price Types";

            var priceTypes = _priceTypeService.GetAllPriceTypes().ToList();

            AddPageHeader(Title);
            return View(priceTypes);
        }

        [HttpPost("", Name = "price-type-add"), Permission("price_type_add"), ValidateAntiForgeryToken,
            /*ValidateModelWithRedirect()*/]
        public async Task<IActionResult> Create(PriceType priceType)
        {
            priceType.Name = priceType.Name;

            priceType = await _priceTypeService.CreateAsync(priceType);
            if (priceType == null)
            {
                ToastError("Price type could not be created!");
            }
            else
            {
                ToastSuccess("Price type created successfully!");
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost("{id}", Name = "price-type-edit"), Permission("price_type_edit"), ValidateAntiForgeryToken,]
        public async Task<IActionResult> Update(ushort id, PriceType priceType)
        {
            if (_priceTypeService.IsDuplicate(priceType))
            {
                ToastError($"A price type with the name '{priceType.Name}', already exists");
                return RedirectToAction(nameof(Index));
            }

            var toUpdate = await _priceTypeService.FindByIdAsync(id);
            toUpdate.Name = priceType.Name;

            toUpdate = await _priceTypeService.UpdateAsync(toUpdate);

            if (toUpdate != null)
            {
                ToastSuccess("Price type updated successfully!");
            }
            else
            {
                ToastError("Price type could not be updated");
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("{id}/delete", Name = "price-type-delete"), Permission("price_type_delete")]
        public async Task<IActionResult> Delete(ushort id)
        {
            var priceType = await _priceTypeService.FindByIdAsync(id);
            if (priceType.IsNull())
            {
                return NotFound();
            }

            await _priceTypeService.DeletePriceTypeAsync(priceType);

            ToastSuccess("Price type deleted successfully!");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("{id}/open-edit-modal"), Permission("price_type_edit")]
        public async Task<JsonResult> EditPriceTypeModal(ushort id)
        {
            var priceType = await _priceTypeService.FindByIdAsync(id);

            return Json(priceType, new JsonSerializerSettings{ContractResolver = null});
        }

        [AcceptVerbs("GET", Route = "validate-price-type-name", Name = "ValidatePriceTypeName")]
        public IActionResult ExistsByDisplayName(string name, ushort id)
        {
            return _priceTypeService.IsDuplicate(name, id)
                ? Json("A price type with this name already exists")
                : Json(true);
        }
    }
}
