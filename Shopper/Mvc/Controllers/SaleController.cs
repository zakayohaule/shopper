using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shopper.Attributes;
using Shopper.Mvc.ViewModels;
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
        public async Task<IActionResult> Index()
        {
            ViewData["Products"] = await _productService.GetProductsSelectListItemsForSaleASync();

            return View();
        }

        [HttpGet("{id}/open-sales-modal")]
        public async Task<IActionResult> OpenSalesFormModal(uint id)
        {
            var skus = await _productService.GetProductSkus(id)
                .Include(sku => sku.SkuAttributes)
                .ThenInclude(skuAtt => skuAtt.Option)
                .ToListAsync();
            var viewModel = new SaleViewModel();
            viewModel.Skus = skus.Select(sku => new SelectListItem
            {
                Value = sku.Id.ToString(),
                Text = $"[ {string.Join(" - ", sku.SkuAttributes.Select(skuAtt => skuAtt.Option.Name))} ] [ Qty: {sku.RemainingQuantity} ] [ {sku.SellingPrice:N0}/- ]"
            }).ToList();

            return PartialView("../Sale/_SalesFormModal", viewModel);
        }
    }
}
