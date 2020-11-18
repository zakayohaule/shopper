using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shared.Extensions.Helpers;
using Shared.Mvc.Entities;
using Shared.Mvc.Entities.Identity;
using Shopper.Attributes;
using Shopper.Services.Interfaces;

namespace Shopper.Mvc.Controllers
{
    [Route("product-categories"), Authorize]
    public class ProductCategoryController : BaseController
    {
        private readonly IProductGroupService _productGroupService;

        public ProductCategoryController(IProductGroupService productGroupService)
        {
            _productGroupService = productGroupService;
        }

        [HttpGet("", Name = "product-category-index"), Permission("product_category_view"), Toast]
        public IActionResult Index()
        {
            Title = "Product Categories";

            var productGroups = _productGroupService.GetAllProductGroups().ToList();

            return View(productGroups);
        }

        [HttpPost("", Name = "product-category-add"), Permission("product_category_create"), ValidateAntiForgeryToken,
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

        [HttpPost("{id}", Name = "product-category-edit"), Permission("product_category_edit"), ValidateAntiForgeryToken,]
        public async Task<IActionResult> Update(ushort id, ProductGroup productGroup)
        {
            if (_productGroupService.IsDuplicate(productGroup.Name, id))
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

            ToastError("Product group could not be updated");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("{id}/delete", Name = "product-category-delete"), Permission("product_category_delete")]
        public async Task<IActionResult> Delete(ushort id)
        {
            var productGroup = await _productGroupService.FindByIdAsync(id);
            if (productGroup.IsNull())
            {
                return NotFound();
            }

            await _productGroupService.DeleteProductGroupAsync(productGroup);

            ToastSuccess("Product group deleted successfully!");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("{id}/open-edit-modal")]
        public async Task<PartialViewResult> EditProductGroupModal(ushort id)
        {
            var productGroup = await _productGroupService.FindByIdAsync(id);

            return PartialView("../ProductGroup/_EditProductGroupModal", productGroup);
        }

        [AcceptVerbs("GET", Route = "validate-product-category-name", Name = "ValidateProductGroupName")]
        public IActionResult ExistsByDisplayName(string name, ushort id)
        {
            return _productGroupService.IsDuplicate(name, id)
                ? Json("A product group with this name already exists")
                : Json(true);
        }
    }
}
