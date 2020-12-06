using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shared.Mvc.Entities;
using Shopper.Attributes;
using Shopper.Mvc.ViewModels;
using Shopper.Services.Interfaces;

namespace Shopper.Mvc.Controllers
{
    [Route("sales")]
    public class SaleController : BaseController
    {
        private readonly ISaleService _saleService;

        public SaleController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        [HttpGet(""), Permission("sale_view"), Toast]
        public async Task<IActionResult> Index()
        {
            var invoice = await _saleService.GetInCompleteInvoiceAsync();
            var productSelectItems = await _saleService.GetProductsSelectListItemsForSaleASync();
            SaleViewModal viewModel = null;
            if (invoice != null)
            {
                viewModel = new SaleViewModal
                {
                    ProductSelectListItems = productSelectItems,
                    Sales = invoice.Sales.ToList(),
                    TotalAmount = invoice.Amount,
                    TotalDiscount = (uint)invoice.Sales.Sum(s => s.Discount)
                };
            }
            else
            {
                viewModel = new SaleViewModal
                {
                    ProductSelectListItems = productSelectItems
                };
            }

            return View(viewModel);
        }

        [HttpGet("{id}/open-sales-modal")]
        public async Task<IActionResult> OpenSalesFormModal(uint id)
        {
            var skus = await _saleService.GetProductSkus(id)
                .Include(sku => sku.SkuAttributes)
                .ThenInclude(skuAtt => skuAtt.Option)
                .ToListAsync();
            var viewModel = new SaleFormViewModel();
            viewModel.Skus = skus.Select(sku => new SelectListItem
            {
                Value = sku.Id.ToString(),
                Text =
                    $"[ {string.Join(" - ", sku.SkuAttributes.Select(skuAtt => skuAtt.Option.Name))} ] [ Qty: {sku.RemainingQuantity} ] [ {sku.SellingPrice:N0}/- ]"
            }).ToList();

            return PartialView("../Sale/_SalesFormModal", viewModel);
        }

        [HttpPost("add-to-cart"), ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToCart(SaleFormViewModel formViewModel)
        {
            // return Ok(formViewModel);
            var saleInvoice = await _saleService.AddToInvoiceAsync(formViewModel);
            return RedirectToAction("Index");
        }
    }
}
