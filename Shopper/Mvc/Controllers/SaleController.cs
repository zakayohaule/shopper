using Microsoft.AspNetCore.Mvc;
using Shopper.Attributes;
using Shopper.Services.Interfaces;

namespace Shopper.Mvc.Controllers
{
    [Route("sales")]
    public class SaleController : BaseController
    {
        private readonly IProductService _productService;

        public SaleController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet(""), Permission("sale_view"), Toast]
        public IActionResult Index()
        {
            ViewData["Products"] = _productService.GetProductsSelectListItemsForSale();

            return View();
        }
    }
}
