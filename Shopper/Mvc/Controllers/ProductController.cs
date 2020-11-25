using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Extensions.Helpers;
using Shared.Mvc.Entities;
using Shared.Mvc.ViewModels;
using Shopper.Attributes;
using Shopper.Services.Interfaces;

namespace Shopper.Mvc.Controllers
{
    [Route("products"), Authorize]
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;
        private readonly IAttributeService _attributeService;

        public ProductController(IProductService productService, IAttributeService attributeService)
        {
            _productService = productService;
            _attributeService = attributeService;
        }

        [Permission("product_view"), Toast]
        [HttpGet("")]
        public IActionResult Index([FromServices] IAttributeService attributeService,
            [FromServices] IProductCategoryService productCategoryService)
        {
            Title = "Products";
            AddPageHeader("Manage products");
            var products = _productService.GetAllProducts()
                .Include(p => p.ProductCategory)
                .Include(p => p.Attributes)
                .ThenInclude(pa => pa.Attribute)
                .ToList();
            ViewData["Attributes"] = attributeService.GetAllAttributeSelectListItems();
            ViewData["Categories"] = productCategoryService.GetProductCategorySelectListItems();
            return View(products);
        }

        [HttpPost(""), Permission("product_add"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product newProduct)
        {
            if (_productService.IsDuplicate(newProduct.Name, newProduct.Id))
            {
                ToastError($"Product with name {newProduct.Name} already exists! Please use another name.");
                return RedirectToAction("Index");
            }

            newProduct = await _productService.CreateProductAsync(newProduct, Request.Form["Attributes"].ToArray());

            // return Ok(newProduct);
            if (newProduct.IsNotNull())
            {
                ToastSuccess($"Product created successfully!");
            }
            else
            {
                ToastError("Product could not be created!");
            }

            return RedirectToAction("Index");
        }


        [AcceptVerbs("GET", Route = "validate-product-name", Name = "ValidateProductName")]
        public IActionResult ExistsByName(string name, ushort id)
        {
            return _productService.IsDuplicate(name, id)
                ? Json("A product with this name already exists")
                : Json(true);
        }
    }
}
