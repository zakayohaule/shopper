using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

            return View(stockedProducts);
        }

        [HttpGet("{id}/open-stock-modal")]
        public async Task<IActionResult> OpenProductStockModal(uint id)
        {
            var stockViewModel = new SkuViewFormModel();
            var product = await _productService.FindByIdWithAttributesAsync(id);
            stockViewModel.Product = product;
            return PartialView("../Stock/_ProductStockForm", stockViewModel);
        }

        [HttpPost(""), Permission("stock_add"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SkuViewFormModel skuViewModel)
        {
            if (!await _productService.ExistsByIdAsync(skuViewModel.Sku.ProductId))
            {
                return NotFound("Product not found");
            }

            var attributeIds = skuViewModel.Attributes.Select(s => ushort.Parse(s.Split("_")[0])).ToList();
            var attributeOptionIds = skuViewModel.Attributes.Select(s => ushort.Parse(s.Split("_")[1])).ToList();
            if (!await _productService.HasAttributes(skuViewModel.Sku.ProductId, attributeIds))
            {
                return BadRequest();
            }

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
    }
}
