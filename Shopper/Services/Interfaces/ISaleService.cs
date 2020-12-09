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
        public Task<List<SelectListItem>> GetProductsSelectListItemsForSaleASync();
        public IQueryable<Sku> GetProductSkus(uint id);
        public Task<SaleInvoice> FindById(ulong id);
        public Task<SaleInvoice> AddToInvoiceAsync(SaleFormViewModel formViewModel);
        public Task<SaleInvoice> GetInCompleteInvoiceAsync();
        public Task<string> GenerateInvoiceNumberAsync();
        public Task<SaleInvoice> ConfirmPaymentAsync(SaleInvoice invoice);
        public Task<string> IsAvailableInStockAsync(int quantity, ulong skuId);
        Task<SaleInvoice> CancelPaymentAsync(SaleInvoice invoice);
    }
}
