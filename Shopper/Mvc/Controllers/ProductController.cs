using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Shopper.Attributes;
using Shopper.Mvc.ViewModels;
using Shopper.Services.Interfaces;

namespace Shopper.Mvc.Controllers
{
    [Route("products"), Authorize]
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [Permission("product_view"), Toast]
        [HttpGet("")]
        public IActionResult Index([FromServices] IAttributeService attributeService,
            [FromServices] IProductGroupService productGroupService)
        {
            Title = "Products";
            AddPageHeader("Manage products");
            var products = _productService.GetAllProducts()
                .AsNoTracking()
                .Include(p => p.ProductType)
                .Include(p => p.Attributes)
                .ThenInclude(pa => pa.Attribute)
                .ToList();
            ViewData["Attributes"] = attributeService.GetAllAttributeSelectListItems();
            ViewData["Groups"] = productGroupService.GetProductGroupSelectListItems();
            return View(products);
        }

        [Permission("product_view"), Toast]
        [HttpGet("{id}")]
        public async Task<IActionResult> Show(uint id)
        {
            var product = await _productService
                .FindByIdAsyncQ(id)
                .Include(p => p.ProductType)
                .ThenInclude(pt => pt.ProductCategory)
                .ThenInclude(pt => pt.ProductGroup)
                .Include(p => p.Images)
                .Include(p => p.Attributes)
                .ThenInclude(p => p.Attribute)
                .Include(p => p.ProductType)
                .Include(p => p.Skus)
                .ThenInclude(s => s.SkuAttributes)
                .ThenInclude(sa => sa.Option)
                .Include(p => p.Skus)
                .ThenInclude(s => s.Sales)
                .ThenInclude(sale => sale.SaleInvoice)
                .FirstOrDefaultAsync();
            if (product == null)
            {
                return NotFound();
            }

            // return Ok(product);
            // ViewProductModel viewModel = await _productService.GetProductViewModelAsync(product);
            return View(product);
        }

        [HttpPost(""), Permission("product_add"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductFormModel newProduct, [FromServices] IFileUploadService fileUploadService)
        {
            // @todo Preselect product category/group if only one is present

            if (_productService.IsDuplicate(newProduct.Name, newProduct.Id))
            {
                ToastError($"Product with name {newProduct.Name} already exists! Please use another name.");
                return RedirectToAction("Index");
            }
            var product = await _productService.CreateProductAsync(newProduct);

            // return Ok(newProduct);
            if (product != null)
            {
                ToastSuccess($"Product created successfully!");
            }
            else
            {
                ToastError("Product could not be created!");
            }

            return RedirectToAction("Index");
        }


        [HttpPost("{id}", Name = "product-edit"), Permission("product_edit"), ValidateAntiForgeryToken,]
        public async Task<IActionResult> Update(uint id, ProductFormModel formModel, [FromServices] IFileUploadService fileUploadService)
        {
            if (_productService.IsDuplicate(formModel.Name, formModel.Id))
            {
                ToastError($"Product with name {formModel.Name} already exists! Please use another name.");
                return RedirectToAction("Index");
            }

            var product = await _productService.FindByIdAsyncQ(formModel.Id).Include(p => p.Attributes)
                .SingleOrDefaultAsync();
            if (product == null)
            {
                return NotFound($"Product with id {id} not found");
            }

            var imageName = "";
            if (formModel.Image != null)
            {
                imageName = await fileUploadService.UploadProductImageAsync(formModel.Image);
            }
            product = await _productService.UpdateProductAsync(formModel, product, imageName);

            ToastSuccess("Product edited successfully!");
            return RedirectToAction("Index");
        }

        [HttpGet("{id}/open-edit-modal")]
        public async Task<JsonResult> EditProductModal(uint id, [FromServices] IAttributeService attributeService,
            [FromServices] IProductCategoryService productCategoryService,
            [FromServices] IProductTypeService productTypeService,
            [FromServices] IProductGroupService productGroupService)
        {
            var product = await _productService.FindByIdAsyncQ(id)
                .Include(p => p.ProductType)
                .ThenInclude(p => p.ProductCategory)
                .Include(p => p.Attributes)
                .SingleAsync();
            ViewData["Attributes"] = attributeService.GetAllAttributeSelectListItems();
            ViewData["Groups"] = productGroupService.GetProductGroupSelectListItems();

            var formData = new
            {
                product.Id,
                product.Name,
                product.ProductType.ProductCategory.ProductGroupId,
                product.ProductType.ProductCategoryId,
                product.ProductTypeId,
                HasExpirationDate = product.HasExpiration,
                Attributes = product.Attributes.Select(at => at.AttributeId),
                CategoryItems = productCategoryService
                    .GetProductCategorySelectListItemsByGroupId(
                        product.ProductType.ProductCategory.ProductGroupId,
                        product.ProductType.ProductCategoryId),
                TypeItems = productTypeService
                    .GetProductTypeSelectListItemsByCategoryId(
                        product.ProductType.ProductCategoryId,
                        product.ProductTypeId)
            };
            return Json(formData, new JsonSerializerSettings {ContractResolver = null});
            // return PartialView("../Product/_EditProductModal", product);
        }

        [AcceptVerbs("GET", Route = "validate-product-name", Name = "ValidateProductName")]
        public IActionResult ExistsByName(string name, ushort id)
        {
            return _productService.IsDuplicate(name, id)
                ? Json("A product with this name already exists")
                : Json(true);
        }

        [HttpGet("{id}/delete", Name = "product-delete"), Permission("product_delete")]
        public async Task<IActionResult> Delete(ushort id)
        {
            var product = await _productService.FindByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            await _productService.DeleteProductAsync(product);

            ToastSuccess("Product deleted successfully!");
            return RedirectToAction(nameof(Index));
        }

        [HttpPost("{id}/add-images"), ValidateAntiForgeryToken]
        public async Task<IActionResult> AddImages(uint id, ImagesUploadModel model)
        {
            var product = await _productService.FindByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            await _productService.AddProductImages(product, model);
            return RedirectToAction("Show", new {id = id});
        }

        [HttpGet("{id}/delete-main-image")]
        public async Task<IActionResult> DeleteMainImage(uint id)
        {
            var product = await _productService.FindByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            await _productService.DeleteProductMainImage(product);

            return RedirectToAction("Show", new {id = id});
        }

        [HttpGet("{id}/delete-other-product-image")]
        public async Task<IActionResult> DeleteOtherImage(ulong id)
        {
            var image = await _productService.FindProductImageByIdAsync(id);
            if (image == null)
            {
                return NotFound();
            }

            await _productService.DeleteProductImageAsync(image);

            return RedirectToAction("Show", new {id = image.ProductId});
        }

        [HttpPost("{id}/update-main-image"), ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateMainImage(uint id, UpdateImageModel imageModel)
        {
            var product = await _productService.FindByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            await _productService.UpdateMainImage(product, imageModel);

            return RedirectToAction("Show", new {id = id});
        }

        [HttpPost("{id}/update-other-image"), ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateOtherImage(ulong id, UpdateImageModel imageModel)
        {
            var image = await _productService.FindProductImageByIdAsync(id);
            if (image == null)
            {
                return NotFound();
            }

            await _productService.UpdateProductImageAsync(image,imageModel);

            return RedirectToAction("Show", new {id = image.ProductId});
        }
    }
}
