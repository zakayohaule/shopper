using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shared.Extensions.Helpers;
using Shared.Mvc.Entities;
using Shopper.Attributes;
using Shopper.Mvc.ViewModels;
using Shopper.Services.Interfaces;
using SixLabors.ImageSharp;

namespace Shopper.Mvc.Controllers
{
    [Route("stocks"), Authorize]
    public class StockController : BaseController
    {
        private readonly IProductService _productService;

        public StockController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet(""), Permission("stock_view"), Toast]
        public IActionResult Index()
        {
            ViewData["Products"] = _productService.GetProductsSelectListItems();
            var stockedProducts = _productService.GetStockedProducts();

            // return Ok(stockedProducts);
            return View(stockedProducts);
        }

        [HttpGet("{id}/open-stock-modal")]
        public async Task<IActionResult> OpenProductStockFormModal(uint id)
        {
            var stockViewModel = new SkuViewFormModel();
            var product = await _productService.FindByIdWithAttributesAsync(id);
            stockViewModel.Product = product;
            stockViewModel.StockDate = DateTime.Today.Date;
            stockViewModel.AttributeSelects = _productService.GetProductAttributeSelects(product, new Sku());
            return PartialView("../Stock/_ProductStockForm", stockViewModel);
        }

        [HttpPost(""), Permission("stock_add"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SkuViewFormModel skuViewModel)
        {
            if (!await _productService.ExistsByIdAsync(skuViewModel.ProductId))
            {
                return NotFound("Product not found");
            }

            var attributeOptionIds = new List<ushort>();
            if (!skuViewModel.Attributes.IsNullOrEmpty())
            {
                var attributeIds = skuViewModel.Attributes.Select(s => ushort.Parse(s.Split("_")[0])).ToList();
                attributeOptionIds = skuViewModel.Attributes.Select(s => ushort.Parse(s.Split("_")[1])).ToList();
                if (!await _productService.HasAttributes(skuViewModel.ProductId, attributeIds))
                {
                    return BadRequest();
                }
            }

            var sku = new Sku
            {
                ProductId = skuViewModel.ProductId,
                Date = skuViewModel.StockDate,
                Quantity = int.Parse(skuViewModel.Quantity.Replace(",", "")),
                BuyingPrice = uint.Parse(skuViewModel.BuyingPrice.Replace(",", "")),
                SellingPrice = uint.Parse(skuViewModel.SellingPrice.Replace(",", "")),
                MaximumDiscount = uint.Parse(skuViewModel.MaximumDiscount.Replace(",", "")),
            };
            skuViewModel.Sku = sku;
            var newSku = await _productService.AddProductToStockAsync(skuViewModel.Sku, attributeOptionIds);
            if (newSku.IsNotNull())
            {
                ToastSuccess("Product added to stock successfully!");
            }
            else
            {
                ToastError("Could not add product to stock!");
            }

            return RedirectToAction("Index");
        }

        [HttpGet("{id}/product-skus"), Permission("stock_view")]
        public async Task<IActionResult> OpenProductSkus(uint id)
        {
            var product = await _productService.FindProductSkusAsync(id);

            return View("ProductSkus", product);
        }
        [HttpGet("{id}/open-product-skus-edit-modal")]
        public async Task<IActionResult> OpenProductSkusEditModal(ulong id)
        {
            var sku = await _productService.FindSkuByIdAsync(id)
                .Include(s => s.SkuAttributes)
                .SingleOrDefaultAsync();
            var product = await _productService.FindByIdWithAttributesAsync(sku.ProductId);
            var stockViewModel = new SkuViewFormModel
            {
               Product = sku.Product,
               Sku = sku,
               BuyingPrice = sku.BuyingPrice.ToString(),
               SellingPrice = sku.SellingPrice.ToString(),
               MaximumDiscount = sku.MaximumDiscount.ToString(),
               Quantity = sku.Quantity.ToString(),
               StockDate = sku.Date,
               ProductId = sku.ProductId,
               AttributeSelects = _productService.GetProductAttributeSelects(product, sku)
            };
            return PartialView("../Stock/_ProductStockForm", stockViewModel);
        }
    }
}
