using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Shared.Extensions.Helpers;
using Shared.Mvc.Entities;
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
            // return Ok(newProduct);
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

        [HttpGet("{id}/open-edit-modal")]
        [AllowAnonymous]
        public async Task<JsonResult> EditProductModal(uint id,[FromServices] IAttributeService attributeService,
            [FromServices] IProductCategoryService productCategoryService)
        {
            var product = await _productService.FindByIdAsyncQ(id).Include(p => p.Attributes).SingleAsync();
            ViewData["Attributes"] = attributeService.GetAllAttributeSelectListItems();
            ViewData["Categories"] = productCategoryService.GetProductCategorySelectListItems();
            ViewBag.EditMode = true;

            var formData = new
            {
                product.Id,
                product.Name,
                product.ProductCategoryId,
                Attributes = product.Attributes.Select(at => at.AttributeId)
            };
            return Json(formData, new JsonSerializerSettings{ContractResolver = null});
            // return PartialView("../Product/_EditProductModal", product);
        }

        [AcceptVerbs("GET", Route = "validate-product-name", Name = "ValidateProductName")]
        public IActionResult ExistsByName(string name, ushort id)
        {
            return _productService.IsDuplicate(name, id)
                ? Json("A product with this name already exists")
                : Json(true);
        }

        [HttpPost("{id}", Name = "product-edit"), Permission("product_edit"), ValidateAntiForgeryToken,]
        public IActionResult Update(uint id, Product product)
        {
            Console.WriteLine($"Product Id is {id}");
            return Ok(product);
        }
    }
}
