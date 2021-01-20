using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shopper.Attributes;
using Shopper.Mvc.Entities;
using Shopper.Services.Interfaces;

namespace Shopper.Mvc.Controllers
{
    [Route("product-groups"), Authorize]
    public class ProductGroupController : BaseController
    {
        private readonly IProductGroupService _productGroupService;

        public ProductGroupController(IProductGroupService productGroupService)
        {
            _productGroupService = productGroupService;
        }

        [HttpGet("", Name = "product-group-index"), Permission("product_group_view"), Toast]
        public IActionResult Index()
        {
            Title = "Product Groups";

            var productGroups = _productGroupService
                .GetAllProductGroups()
                .ToList();

            AddPageHeader(Title);
            return View(productGroups);
        }

        [HttpPost("", Name = "product-group-add"), Permission("product_group_add"), ValidateAntiForgeryToken,
            /*ValidateModelWithRedirect()*/]
        public async Task<IActionResult> Create(ProductGroup productGroup)
        {
            productGroup.Name = productGroup.Name;

            productGroup = await _productGroupService.CreateAsync(productGroup);
            if (productGroup == null)
            {
                ToastError("Product group could not be created!");
            }
            else
            {
                ToastSuccess("Product group created successfully!");
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost("{id}", Name = "product-group-edit"), Permission("product_group_edit"), ValidateAntiForgeryToken,]
        public async Task<IActionResult> Update(ushort id, ProductGroup productGroup)
        {
            if (_productGroupService.IsDuplicate(productGroup))
            {
                ToastError($"A product group with the name '{productGroup.Name}', already exists");
                return RedirectToAction(nameof(Index));
            }

            var toUpdate = await _productGroupService.FindByIdAsync(id);
            toUpdate.Name = productGroup.Name;

            toUpdate = await _productGroupService.UpdateAsync(toUpdate);

            if (toUpdate != null)
            {
                ToastSuccess("Product group updated successfully!");
            }
            else
            {
                ToastError("Product group could not be updated");
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("{id}/delete", Name = "product-group-delete"), Permission("product_group_delete")]
        public async Task<IActionResult> Delete(ushort id)
        {
            var productGroup = await _productGroupService.FindByIdAsync(id);
            if (productGroup == null)
            {
                return NotFound();
            }

            await _productGroupService.DeleteProductGroupAsync(productGroup);

            ToastSuccess("Product group deleted successfully!");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("{id}/open-edit-modal"), Permission("product_group_edit")]
        public async Task<JsonResult> EditProductGroupModal(ushort id)
        {
            var productGroup = await _productGroupService.FindByIdAsync(id);

            return Json(productGroup, new JsonSerializerSettings{ContractResolver = null});
        }

        [AcceptVerbs("GET", Route = "validate-product-group-name", Name = "ValidateProductGroupName")]
        public IActionResult ExistsByDisplayName(string name, ushort id)
        {
            return _productGroupService.IsDuplicate(name, id)
                ? Json("A product group with this name already exists")
                : Json(true);
        }
    }
}
