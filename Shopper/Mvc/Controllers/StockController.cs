using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Extensions.Helpers;
using Shared.Mvc.Entities;
using Shopper.Attributes;
using Shopper.Mvc.ViewModels;
using Shopper.Services.Interfaces;

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

            AddPageHeader("");
            return View(stockedProducts);
        }

        [HttpGet("{id}/open-stock-modal")]
        public async Task<IActionResult> OpenProductStockFormModal(uint id)
        {
            var stockViewModel = new SkuViewFormModel();
            var product = await _productService.FindByIdWithAttributesAsync(id);
            stockViewModel.Product = product;
            stockViewModel.StockDate = DateTime.Today.Date;
            stockViewModel.ExpirationDate = DateTime.Now.AddYears(1);
            stockViewModel.MaximumDiscount = 0.ToString();
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

            var attributeOptionIds = await GetAttributeIdsFromViewModelAsync(skuViewModel);
            if (attributeOptionIds.IsNull())
            {
                return BadRequest();
            }

            skuViewModel.Sku = GetSkuFromViewModel(skuViewModel);

            var newSku = await _productService.AddProductToStockAsync(skuViewModel, attributeOptionIds);

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


        [HttpPost("{id}/edit-sku"), Permission("stock_edit"), ValidateAntiForgeryToken]
        public async Task<IActionResult> EditStockItem(ulong id, SkuViewFormModel skuViewModel)
        {
            // @todo validate stock item quantity
            var sku = await _productService.FindSkuByIdAsync(id).SingleOrDefaultAsync();
            if (sku.IsNull())
            {
                return NotFound();
            }

            var attributeOptionIds = await GetAttributeIdsFromViewModelAsync(skuViewModel);
            if (attributeOptionIds.IsNull())
            {
                return BadRequest();
            }

            var updated = GetSkuFromViewModel(skuViewModel);

            sku = await _productService.UpdateStockItemAsync(sku,skuViewModel, attributeOptionIds);
            if (sku.IsNull())
            {
                ToastError("Stock item could not be updated. Please try again or contact system administrator");
            }

            return RedirectToAction("OpenProductSkus", new {id = sku.ProductId});
        }

        [HttpGet("{id}/product-skus"), Permission("stock_view")]
        public async Task<IActionResult> OpenProductSkus(uint id)
        {
            var product = await _productService.FindProductSkusAsync(id);

            return View("ProductSkus", product);
        }
        [HttpGet("{id}/open-product-skus-edit-modal"), Permission("stock_edit")]
        public async Task<IActionResult> OpenProductSkusEditModal(ulong id)
        {
            var sku = await _productService.FindSkuByIdAsync(id)
                .Include(s => s.SkuAttributes)
                .Include(s => s.Product)
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
            return PartialView("../Stock/_EditStockItem", stockViewModel);
        }


        [HttpGet("{id}/delete-sku", Name = "sku-delete"), Permission("stock_delete")]
        public async Task<IActionResult> DeleteSku(ulong id)
        {
            var sku = await _productService.FindSkuByIdAsync(id).SingleOrDefaultAsync();
            if (sku.IsNull())
            {
                return NotFound();
            }

            await _productService.DeleteSkuAsync(sku);
            ToastSuccess("Stock item deleted successfully!");
            return RedirectToAction("OpenProductSkus", new {id = sku.ProductId});
        }

        private async Task<List<ushort>> GetAttributeIdsFromViewModelAsync(SkuViewFormModel skuViewModel)
        {
            var attributeOptionIds = new List<ushort>();
            if (skuViewModel.Attributes.IsNullOrEmpty()) return attributeOptionIds;
            var attributeIds = skuViewModel.Attributes.Select(s => ushort.Parse(s.Split("_")[0])).ToList();
            attributeOptionIds = skuViewModel.Attributes.Select(s => ushort.Parse(s.Split("_")[1])).ToList();
            if (!await _productService.HasAttributes(skuViewModel.ProductId, attributeIds))
            {
                return null;
            }

            return attributeOptionIds;
        }

        private Sku GetSkuFromViewModel(SkuViewFormModel skuViewModel)
        {
            return new Sku
            {
                ProductId = skuViewModel.ProductId,
                Date = skuViewModel.StockDate,
                Quantity = int.Parse(skuViewModel.Quantity.Replace(",", "")),
                BuyingPrice = uint.Parse(skuViewModel.BuyingPrice.Replace(",", "")),
                SellingPrice = uint.Parse(skuViewModel.SellingPrice.Replace(",", "")),
                MaximumDiscount = uint.Parse(skuViewModel.MaximumDiscount.Replace(",", "")),
                LowStockAmount = skuViewModel.ReceiveLowStockAlert ? skuViewModel.LowStockQuantity : null
            };
        }

        [AcceptVerbs("GET", Route = "validate-selling-price", Name = "ValidateSellingPrice")]
        public IActionResult ValidateSellingPrice(string sellingPrice, string buyingPrice)
        {
            if (buyingPrice.IsNullOrEmpty())
            {
                return Json("Buying price is required.");
            }
            var sPrice = int.Parse(sellingPrice.Replace(",", ""));
            var bPrice = int.Parse(buyingPrice.Replace(",", ""));
            if (sPrice >= bPrice)
            {
                return Json(true);
            }

            return Json("Selling price can't be less than the buying price.");
        }

        [AcceptVerbs("GET", Route = "validate-discount-price", Name = "ValidateMaximumDiscount")]
        public IActionResult ValidateDiscount(string maximumDiscount, string sellingPrice, string buyingPrice)
        {
            if (buyingPrice.IsNullOrEmpty())
            {
                return Json("Buying price is required.");
            }

            if (sellingPrice.IsNullOrEmpty())
            {
                return Json("Selling price is required.");
            }
            var sPrice = int.Parse(sellingPrice.Replace(",", ""));
            var bPrice = int.Parse(buyingPrice.Replace(",", ""));
            var dCount = int.Parse(maximumDiscount.Replace(",", ""));
            var expectedProfit = sPrice - bPrice;
            if (expectedProfit < 0)
            {
                return Json($"Invalid selling price");
            }
            if (dCount <= expectedProfit)
            {
                return Json(true);
            }

            return Json($"Maximum discount can't be more than the expected profit.({expectedProfit.ToString("N0")})");
        }
    }
}
