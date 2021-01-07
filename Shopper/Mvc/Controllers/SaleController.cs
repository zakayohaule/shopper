using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shared.Common;
using Shared.Extensions.Helpers;
using Shopper.Attributes;
using Shopper.Extensions.Helpers;
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
            AddPageHeader("Sale invoice list");
            var invoices = await _saleService.GetSaleInvoicesAsync();
            return View(invoices);
        }

        [HttpGet("{id}"), Permission("sale_invoice_view"), Toast]
        public async Task<IActionResult> InvoiceSales(ulong id)
        {
            AddPageHeader("Invoice sales list");
            var invoice = await _saleService.GetInvoiceAsQueryable(id)
                .AsNoTracking()
                .Include(si => si.Sales)
                .ThenInclude(s => s.Sku)
                .ThenInclude(s => s.Product)
                .Include(si => si.Sales)
                .ThenInclude(s => s.Sku)
                .ThenInclude(s => s.SkuAttributes)
                .ThenInclude(s => s.Option)
                .FirstOrDefaultAsync();

            if (invoice == null)
            {
                return NotFound("Invoice not found");
            }
            return View(invoice);
        }

        [HttpGet("add-sale"), Permission("sale_sell"), Toast]
        public async Task<IActionResult> Create()
        {
            var invoice = await _saleService.GetInCompleteInvoiceAsync();
            var productSelectItems = await _saleService.GetProductsSelectListItemsForSaleASync();
            SaleViewModal viewModel;
            if (invoice != null)
            {
                viewModel = new SaleViewModal
                {
                    InvoiceId = invoice.Id,
                    ProductSelectListItems = productSelectItems,
                    Sales = invoice.Sales.ToList(),
                    TotalAmount = invoice.Amount,
                    TotalDiscount = (uint) invoice.Sales.Sum(s => s.Discount)
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

        [HttpGet("{id}/open-sales-modal"), Permission("sale_sell")]
        public async Task<IActionResult> OpenSalesFormModal(uint id)
        {
            var viewModel = await GetSaleFormModalFromSaleIdAsync(id);
            return PartialView("../Sale/_SalesFormModal", viewModel);
        }

        [HttpGet("{id}/open-sale-edit-modal"), Permission("sale_edit")]
        public async Task<IActionResult> OpenSaleEditModal(ulong id)
        {
            var sale = await _saleService.FindSaleByIdAsync(id);
            if (sale.IsNull())
            {
                return NotFound();
            }

            var viewModel = await GetSaleFormModalFromSaleIdAsync(sale.Sku.ProductId);
            viewModel.Id = sale.Id;
            viewModel.Price = sale.Price.ToString("N0");
            viewModel.Quantity = sale.Quantity;
            viewModel.SkuId = sale.SkuId;
            return PartialView("../Sale/_EditSaleModal", viewModel);
        }

        [HttpPost("add-to-cart"), ValidateAntiForgeryToken, Permission("sale_sell")]
        public async Task<IActionResult> AddToCart(SaleFormViewModel formViewModel)
        {
            try
            {
                await _saleService.AddToInvoiceAsync(formViewModel, HttpContext.GetUserId(), HttpContext.GetCurrentTenant());
            }
            catch (OutOfStockException e)
            {
                ToastError(e.Message);
            }

            return RedirectToAction("Create");
        }

        [HttpPost("{id}/update-sale"), ValidateAntiForgeryToken, Permission("sale_edit")]
        public async Task<IActionResult> UpdateSale(ulong id, SaleFormViewModel viewModel)
        {
            var sale = await _saleService.FindSaleByIdAsync(id);
            if (sale.IsNull())
            {
                return NotFound();
            }

            sale = await _saleService.UpdateSaleAsync(sale, viewModel);

            return RedirectToAction("Index");
        }

        [HttpPost("{id}/submit-payment"), ValidateAntiForgeryToken, Permission("sale_sell")]
        public async Task<IActionResult> SubmitPayment(ulong id, string action)
        {
            var invoice = await _saleService.FindInvoiceByIdAsync(id);
            if (invoice == null)
            {
                return NotFound($"Invoice with id {id} not found");
            }

            if (!action.Equals("submit") && !action.Equals("cancel"))
            {
                return BadRequest("Invalid payment action");
            }

            if (action.Equals("submit"))
            {
                invoice = await _saleService.ConfirmPaymentAsync(invoice);
                return RedirectToAction("InvoiceSales", new {id = invoice.Id});
            }

            if (action.Equals("cancel"))
            {
                invoice = await _saleService.CancelPaymentAsync(invoice);
            }

            if (invoice == null)
            {
                ToastError($"Could not {action} invoice payment. Please try again or contact system administrator");
            }

            return RedirectToAction("Index");
            // return Ok($"Confirm payment for invoice with {invoice.Id} with total amount of {invoice.Amount}");
        }

        [HttpPost("{id}/change-date"), ValidateAntiForgeryToken, Permission("sale_invoice_change_date")]
        public async Task<IActionResult> ChangeInvoiceDate(ulong id, InvoiceDateModel formModel)
        {
            var invoice = await _saleService.FindInvoiceByIdAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }

            invoice.Date = formModel.InvoiceDate;
            await _saleService.UpdateInvoiceDateAsync(invoice);
            return RedirectToAction("InvoiceSales", new {id = invoice.Id});
        }

        [AcceptVerbs("GET", Route = "check-stock-quantity", Name = "CheckStockQuantity")]
        public async Task<IActionResult> CheckStockQuantity(int quantity, ulong skuId)
        {
            var message = await _saleService.IsAvailableInStockAsync(quantity, skuId);
            if (message == null)
            {
                return Json(true);
            }

            return Json(message);
        }

        public IActionResult DeleteSale()
        {
            throw new NotImplementedException();
        }

        private async Task<SaleFormViewModel> GetSaleFormModalFromSaleIdAsync(uint id)
        {
            var skus = await _saleService.GetProductSkus(id)
                .AsNoTracking()
                .Include(sku => sku.SkuAttributes)
                .ThenInclude(skuAtt => skuAtt.Option)
                .ToListAsync();

            return new SaleFormViewModel
            {
                Skus = skus.Select(sku => new SelectListItem
                {
                    Value = sku.Id.ToString(),
                    Text =
                        $"[ {string.Join(" - ", sku.SkuAttributes.Select(skuAtt => skuAtt.Option.Name))} ] [ Qty: {sku.RemainingQuantity} ]"
                }).ToList(),
                SkuPrices = skus.Select(sku => new SelectListItem
                {
                    Value = sku.Id.ToString(),
                    Text = sku.SellingPrice.ToString()
                }).ToList()
            };
        }
    }
}
