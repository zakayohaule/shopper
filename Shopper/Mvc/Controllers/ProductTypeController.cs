using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Shopper.Attributes;
using Shopper.Mvc.Entities;
using Shopper.Services.Interfaces;

namespace Shopper.Mvc.Controllers
{
    [Route("product-types"), Authorize]
    public class ProductTypeController : BaseController
    {
        private readonly IProductTypeService _productTypeService;
        private readonly IProductCategoryService _productCategoryService;

        public ProductTypeController(IProductTypeService productTypeService,
            IProductCategoryService productCategoryService)
        {
            _productTypeService = productTypeService;
            _productCategoryService = productCategoryService;
        }

        [HttpGet("", Name = "product-types-index"), Permission("product_type_view"), Toast]
        public IActionResult Index()
        {
            Title = "Product Types";
            AddPageHeader(Title);
            var productTypes = _productTypeService
                .GetAllProductTypes()
                .AsNoTracking()
                .Include(pc => pc.ProductCategory)
                .ToList();
            ViewData["ProductCategories"] = _productCategoryService.GetProductCategorySelectListItems();
            return View(productTypes);
        }

        [HttpPost("", Name = "product-types-add"), Permission("product_type_add"), ValidateAntiForgeryToken,
            /*ValidateModelWithRedirect()*/]
        public async Task<IActionResult> Create(ProductType productType)
        {
            productType.Name = productType.Name;

            productType = await _productTypeService.CreateAsync(productType);
            if (productType == null)
            {
                ToastError("Product type could not be created!");
            }
            else
            {
                ToastSuccess("Product type created successfully!");
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost("{id}", Name = "product-types-edit"), Permission("product_type_edit"),
         ValidateAntiForgeryToken,]
        public async Task<IActionResult> Update(ushort id, ProductType productType)
        {
            if (!await _productTypeService.ExistsByIdAsync(id))
            {
                return NotFound("Product type not found");
            }
            if (_productTypeService.IsDuplicate(productType))
            {
                ToastError($"A product type with the name '{productType.Name}', already exists");
                return RedirectToAction(nameof(Index));
            }

            productType.Id = id;
            productType = await _productTypeService.UpdateAsync(productType);

            if (productType != null)
            {
                ToastSuccess("Product type updated successfully!");
            }
            else
            {
                ToastError("Product type could not be updated");
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("{id}/delete", Name = "product-types-delete"), Permission("product_type_delete")]
        public async Task<IActionResult> Delete(ushort id)
        {
            if (!await _productTypeService.ExistsByIdAsync(id))
            {
                return NotFound();
            }

            await _productTypeService.DeleteProductTypeAsync(new ProductType{Id = id});

            ToastSuccess("Product type deleted successfully!");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("{id}/open-edit-modal"), Permission("product_type_edit")]
        public async Task<JsonResult> EditProductTypeModal(ushort id)
        {
            var productType = await _productTypeService.FindByIdAsync(id);

            return Json(productType, new JsonSerializerSettings{ContractResolver = null});
        }

        [AcceptVerbs("GET", Route = "validate-product-types-name", Name = "ValidateProductTypeName")]
        public IActionResult ExistsByDisplayName(string name, ushort id)
        {
            return _productTypeService.IsDuplicate(name, id)
                ? Json("A product type with this name already exists")
                : Json(true);
        }

        [HttpGet("{categoryId}/ajax/{selectedTypeId?}")]
        public IActionResult ProductTypeByCategoryIdAjax(ushort categoryId, ushort? selectedTypeId)
        {
            var selectListItems = _productTypeService.GetProductTypeSelectListItemsByCategoryId(categoryId, selectedTypeId).ToList();
            return Json(selectListItems);
        }
    }
}
