using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
        private readonly IProductGroupService _productGroupService;

        public ProductCategoryController(IProductCategoryService productCategoryService,
            IProductGroupService productGroupService)
        {
            _productCategoryService = productCategoryService;
            _productGroupService = productGroupService;
        }

        [HttpGet("", Name = "product-category-index"), Permission("product_category_view"), Toast]
        public IActionResult Index()
        {
            Title = "Product Categories";
            AddPageHeader(Title);
            var productCategories = _productCategoryService.GetAllProductCategories().Include(pc => pc.ProductGroup)
                .ToList();
            ViewData["ProductGroups"] = _productGroupService.GetProductGroupSelectListItems();
            return View(productCategories);
        }

        [HttpPost("", Name = "product-category-add"), Permission("product_category_add"), ValidateAntiForgeryToken,
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

        [HttpPost("{id}", Name = "product-category-edit"), Permission("product_category_edit"),
         ValidateAntiForgeryToken,]
        public async Task<IActionResult> Update(ushort id, ProductCategory productCategory)
        {
            if (!await _productCategoryService.ExistsByIdAsync(id))
            {
                return NotFound("Product category not found");
            }
            if (_productCategoryService.IsDuplicate(productCategory))
            {
                ToastError($"A product category with the name '{productCategory.Name}', already exists");
                return RedirectToAction(nameof(Index));
            }

            productCategory.Id = id;
            productCategory = await _productCategoryService.UpdateAsync(productCategory);

            if (productCategory != null)
            {
                ToastSuccess("Product category updated successfully!");
            }
            else
            {
                ToastError("Product category could not be updated");
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("{id}/delete", Name = "product-category-delete"), Permission("product_category_delete")]
        public async Task<IActionResult> Delete(ushort id)
        {
            if (!await _productCategoryService.ExistsByIdAsync(id))
            {
                return NotFound();
            }

            await _productCategoryService.DeleteProductCategoryAsync(new ProductCategory{Id = id});

            ToastSuccess("Product category deleted successfully!");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("{id}/open-edit-modal")]
        public async Task<JsonResult> EditProductCategoryModal(ushort id)
        {
            var productCategory = await _productCategoryService.FindByIdAsync(id);

            return Json(productCategory, new JsonSerializerSettings{ContractResolver = null});
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
