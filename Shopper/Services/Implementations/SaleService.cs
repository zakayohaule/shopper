using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shared.Common;
using Shared.Mvc.Entities;
using Shopper.Database;
using Shopper.Mvc.ViewModels;
using Shopper.Services.Interfaces;

namespace Shopper.Services.Implementations
{
    public class SaleService : ISaleService
    {
        private readonly ApplicationDbContext _dbContext;

        public SaleService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<SelectListItem>> GetProductsSelectListItemsForSaleASync()
        {
            return await _dbContext.Products
                .Include(prod => prod.Skus)
                .Where(prod => prod.Skus.Any(sku => sku.RemainingQuantity > 0))
                .Select(prod => new SelectListItem
                {
                    Text = prod.Name,
                    Value = prod.Id.ToString()
                }).ToListAsync();
        }

        public IQueryable<Sku> GetProductSkus(uint id)
        {
            return _dbContext.Skus
                .Include(sku => sku.Product)
                .Where(sku => sku.RemainingQuantity > 0 && sku.ProductId == id)
                .AsQueryable();
        }

        public async Task<SaleInvoice> FindInvoiceByIdAsync(ulong id)
        {
            return await _dbContext.SaleInvoices.FindAsync(id);
        }

        public async Task<Sale> FindSaleByIdAsync(ulong id)
        {
            return await _dbContext.Sales.Include(s => s.Sku).FirstOrDefaultAsync(s => s.Id.Equals(id));
        }

        public async Task<SaleInvoice> AddToInvoiceAsync(SaleFormViewModel formViewModel)
        {
            var sku = await _dbContext.Skus.FirstOrDefaultAsync(sku1 => sku1.Id == formViewModel.SkuId);
            if (sku == null)
            {
                return null;
            }

            try
            {
                await _dbContext.Database.BeginTransactionAsync();
                var price = uint.Parse(formViewModel.Price.Replace(",", ""));
                var sale = new Sale
                {
                    Price = price,
                    Quantity = formViewModel.Quantity,
                    SkuId = formViewModel.SkuId,
                    Discount = sku.SellingPrice > price
                        ? (uint) ((sku.SellingPrice - price) * formViewModel.Quantity)
                        : 0,
                    Profit = (price - sku.BuyingPrice) * formViewModel.Quantity
                };
                SaleInvoice invoice = null;
                if (await _dbContext.SaleInvoices.AnyAsync(si => !si.IsCompleted))
                {
                    invoice = await _dbContext.SaleInvoices
                        .FirstAsync(si => !si.IsCompleted);
                }

                if (invoice == null)
                {
                    invoice = new SaleInvoice
                    {
                        Amount = (ulong) (sale.Price * sale.Quantity), Date = DateTime.Now,
                        Number = await GenerateInvoiceNumberAsync()
                    };
                    await _dbContext.SaleInvoices.AddAsync(invoice);
                }
                else
                {
                    invoice.Amount += (ulong) (sale.Price * sale.Quantity);
                    _dbContext.SaleInvoices.Update(invoice);
                }

                sale.SaleInvoice = invoice;
                await _dbContext.Sales.AddAsync(sale);
                sku.RemainingQuantity -= sale.Quantity;
                if (sku.RemainingQuantity < 0)
                {
                    sku.RemainingQuantity += sale.Quantity;
                    throw new OutOfStockException(
                        $"There are less than {sale.Quantity} items left in stock.( {sku.RemainingQuantity} left)");
                }

                _dbContext.Skus.Update(sku);
                await _dbContext.SaveChangesAsync();
                _dbContext.Database.CommitTransaction();
                return invoice;
            }
            catch (OutOfStockException e)
            {
                throw;
            }
            catch (Exception e)
            {
                _dbContext.Database.RollbackTransaction();
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<Sale> UpdateSaleAsync(Sale sale, SaleFormViewModel viewModel)
        {
            var price = uint.Parse(viewModel.Price.Replace(",", ""));
            var priceChange = (int)price - (int)sale.Price;
            var quantityChange = viewModel.Quantity - sale.Quantity;
            var saleChanged = priceChange != 0 || quantityChange != 0;

            if (saleChanged)
            {
                await _dbContext.Entry(sale).Reference(s => s.SaleInvoice).LoadAsync();
                var sku = sale.Sku;
                var invoice = sale.SaleInvoice;
                sale.Profit = (price - sku.BuyingPrice) * viewModel.Quantity;
                sale.Discount = sku.SellingPrice > price
                    ? (uint) ((sku.SellingPrice - price) * viewModel.Quantity)
                    : 0;

                //Update Sku
                sku.RemainingQuantity -= quantityChange;

                invoice.Amount -= (ulong) (sale.Price * sale.Quantity);
                invoice.Amount += (ulong) (viewModel.Quantity * price);
                /*invoice.Amount -= (ulong)(sale.Price * viewModel.Quantity);
                invoice.Amount += (ulong)(price * viewModel.Quantity);*/
                sale.Quantity = viewModel.Quantity;
                sale.Price = price;
            }

            var updated = _dbContext.Sales.Update(sale);
            await _dbContext.SaveChangesAsync();
            return updated.Entity;
        }

        public async Task<SaleInvoice> GetInCompleteInvoiceAsync()
        {
            if (await _dbContext.SaleInvoices.AnyAsync(si => !si.IsCompleted))
            {
                return await _dbContext.SaleInvoices
                    .Include(si => si.Sales)
                    .ThenInclude(s => s.Sku)
                    .ThenInclude(s => s.Product)
                    .FirstAsync(si => !si.IsCompleted);
            }

            return null;
        }

        public async Task<string> GenerateInvoiceNumberAsync()
        {
            ulong id = 1;
            var latest = await _dbContext.SaleInvoices.OrderByDescending(si => si.Id).FirstOrDefaultAsync();
            if (latest != null)
            {
                id = latest.Id;
            }

            return $"{id++}_{DateTimeOffset.Now.ToUnixTimeSeconds()}";
        }

        public async Task<SaleInvoice> ConfirmPaymentAsync(SaleInvoice invoice)
        {
            invoice.IsCompleted = true;
            invoice.IsCanceled = false;
            var updatedInvoice = _dbContext.SaleInvoices.Update(invoice);

            await _dbContext.SaveChangesAsync();
            return updatedInvoice.Entity;
        }

        public async Task<SaleInvoice> CancelPaymentAsync(SaleInvoice invoice)
        {
            try
            {
                await _dbContext.Database.BeginTransactionAsync();
                invoice.IsCompleted = true;
                invoice.IsCanceled = true;
                var updatedInvoice = _dbContext.SaleInvoices.Update(invoice);

                await _dbContext.Entry(invoice)
                    .Collection(i => i.Sales)
                    .Query()
                    .Include(sale => sale.Sku)
                    .LoadAsync();

                foreach (var sale in invoice.Sales)
                {
                    sale.IsConfirmed = false;
                    sale.Sku.RemainingQuantity += sale.Quantity;
                    _dbContext.Sales.Update(sale);
                }

                await _dbContext.SaveChangesAsync();
                _dbContext.Database.CommitTransaction();
                return updatedInvoice.Entity;
            }
            catch (Exception e)
            {
                _dbContext.Database.RollbackTransaction();
                Console.WriteLine(e);
                return null;
            }
        }

        public async Task<string> IsAvailableInStockAsync(int quantity, ulong skuId)
        {
            var sku = await _dbContext.Skus.FindAsync(skuId);
            var isAvailable = quantity <= sku.RemainingQuantity;
            if (isAvailable)
            {
                return null;
            }

            return $"There are only {sku.RemainingQuantity} items in stock!";
        }

        /*private void UpdateSaleQuantity(Sale sale)
        {
            var invoice = sale.SaleInvoice;
            var sku = sale.Sku;

        }

        private void UpdateSalePrice(Sale sale)
        {

        }*/
    }
}
