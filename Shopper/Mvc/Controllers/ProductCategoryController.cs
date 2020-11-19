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
        private readonly IProductCategoryService _productCategoryService;

        public ProductCategoryController(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        [HttpGet("", Name = "product-category-index"), Permission("product_category_view"), Toast]
        public IActionResult Index()
        {
            Title = "Product Categories";

            var productCategories = _productCategoryService.GetAllProductCategories().ToList();

            return View(productCategories);
        }

        [HttpPost("", Name = "product-category-add"), Permission("product_category_create"), ValidateAntiForgeryToken,
            /*ValidateModelWithRedirect()*/]
        public async Task<IActionResult> Create(ProductCategory productCategory)
        {
            productCategory.Name = productCategory.Name;

            productCategory = await _productCategoryService.CreateAsync(productCategory);
            if (productCategory == null)
            {
                ToastError("Product category could not be created!");
            }
            else
            {
                ToastSuccess("Product category created successfully!");
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost("{id}", Name = "product-category-edit"), Permission("product_category_edit"), ValidateAntiForgeryToken,]
        public async Task<IActionResult> Update(ushort id, ProductCategory productCategory)
        {
            if (_productCategoryService.IsDuplicate(productCategory.Name, id))
            {
                ToastError($"A product category with the name '{productCategory.Name}', already exists");
                return RedirectToAction(nameof(Index));
            }

            var toUpdate = await _productCategoryService.FindByIdAsync(id);
            toUpdate.Name = productCategory.Name;

            toUpdate = await _productCategoryService.UpdateAsync(toUpdate);

            if (toUpdate != null)
            {
                ToastSuccess("Product category updated successfully!");
            }

            ToastError("Product category could not be updated");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("{id}/delete", Name = "product-category-delete"), Permission("product_category_delete")]
        public async Task<IActionResult> Delete(ushort id)
        {
            var productCategory = await _productCategoryService.FindByIdAsync(id);
            if (productCategory.IsNull())
            {
                return NotFound();
            }

            await _productCategoryService.DeleteProductCategoryAsync(productCategory);

            ToastSuccess("Product category deleted successfully!");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("{id}/open-edit-modal")]
        public async Task<PartialViewResult> EditProductCategoryModal(ushort id)
        {
            var productCategory = await _productCategoryService.FindByIdAsync(id);

            return PartialView("../ProductCategory/_EditProductCategoryModal", productCategory);
        }

        [AcceptVerbs("GET", Route = "validate-product-category-name", Name = "ValidateProductCategoryName")]
        public IActionResult ExistsByDisplayName(string name, ushort id)
        {
            return _productCategoryService.IsDuplicate(name, id)
                ? Json("A product category with this name already exists")
                : Json(true);
        }
    }
}
