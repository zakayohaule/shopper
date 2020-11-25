using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopper.Attributes;
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
            var product = await _productService.FindByIdAsync(id);
            return PartialView("../Stock/_ProductStockForm", product);
        }
    }
}
