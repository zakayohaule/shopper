using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shared.Mvc.Entities;
using Shopper.Mvc.ViewModels;

namespace Shopper.Services.Interfaces
{
    public interface ISaleService
    {
        public Task<List<SaleInvoice>> GetSaleInvoicesAsync();
        public Task<List<SelectListItem>> GetProductsSelectListItemsForSaleASync();
        public IQueryable<Sku> GetProductSkus(uint id);
        public IQueryable<SaleInvoice> GetInvoiceAsQueryable(ulong id);
        public Task<SaleInvoice> FindInvoiceByIdAsync(ulong id);
        public Task<Sale> FindSaleByIdAsync(ulong id);
        public IQueryable<Sale> FindSalesByInvoiceIdAsQueryable(ulong id);
        public Task<SaleInvoice> AddToInvoiceAsync(SaleFormViewModel formViewModel, long userId, Tenant tenant);
        public Task<SaleInvoice> UpdateInvoiceDateAsync(SaleInvoice invoice);
        public Task<Sale> UpdateSaleAsync(Sale sale, SaleFormViewModel viewModel);
        public Task<SaleInvoice> GetInCompleteInvoiceAsync();
        public Task<string> GenerateInvoiceNumberAsync(string tenantCode);
        public Task<SaleInvoice> ConfirmPaymentAsync(SaleInvoice invoice);
        public Task<string> IsAvailableInStockAsync(int quantity, ulong skuId);
        public Task<SaleInvoice> CancelPaymentAsync(SaleInvoice invoice);
        IQueryable<Sale> GetSalesAsQueryable();
    }
}
