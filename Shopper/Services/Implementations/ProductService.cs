using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.WebEncoders.Testing;
using Shared.Mvc.Entities;
using Shopper.Database;
using Shopper.Mvc.ViewModels;
using Shopper.Services.Interfaces;

namespace Shopper.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Product> GetAllProducts()
        {
            return _dbContext.Products.AsQueryable();
        }

        public List<SelectListItem> GetProductsSelectListItems()
        {
            return _dbContext.Products
                .Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.Name
                }).ToList();
        }

        public List<Product> GetStockedProducts()
        {
            return GetAllProducts()
                .Include(p => p.ProductCategory)
                .Include(p => p.Skus)
                .ThenInclude(sku => sku.SkuAttributes)
                .ThenInclude(skuAtt => skuAtt.Option)
                .Where(p => p.Skus.Any(sku => sku.RemainingQuantity > 0))
                .ToList();
        }

        public bool IsDuplicate(string name, uint id)
        {
            return _dbContext
                .Products
                .Any(pg => pg.Name.Equals(name, StringComparison.OrdinalIgnoreCase) &&
                           pg.Id != id);
        }

        public async Task<bool> ExistsByIdAsync(uint id)
        {
            return await _dbContext.Products.AnyAsync(pc => pc.Id.Equals(id));
        }

        public async Task<Product> FindByIdAsync(uint id)
        {
            return await _dbContext.Products.FindAsync(id);
        }

        public async Task<Product> FindByIdWithAttributesAsync(uint id)
        {
            return await _dbContext
                .Products
                .Include(p => p.Attributes)
                .ThenInclude(pa => pa.Attribute)
                .ThenInclude(ao => ao.AttributeOptions)
                .FirstAsync(p => p.Id.Equals(id));
        }

        public async Task<Product> FindProductSkusAsync(uint productId)
        {
            var product = await _dbContext
                .Products
                .Include(p => p.Skus)
                .ThenInclude(sku => sku.SkuAttributes)
                .ThenInclude(skuAtt => skuAtt.Option)
                .FirstAsync(p => p.Id.Equals(productId));
            product.Skus = product.Skus.OrderByDescending(sku => sku.CreatedAt).ToList();
            return product;
        }

        public async Task<Product> CreateProductAsync(Product newProduct, string[] attributes = null)
        {
            var product = await _dbContext.Products.AddAsync(newProduct);

            // return product.Entity;
            if (attributes != null && attributes.Length > 0)
            {
                var productAttributes = new List<ProductAttribute>();
                foreach (var attribute in attributes)
                {
                    productAttributes.Add(new ProductAttribute
                    {
                        AttributeId = ushort.Parse(attribute),
                        Product = product.Entity,
                    });
                }

                await _dbContext.ProductAttributes.AddRangeAsync(productAttributes);
            }

            await _dbContext.SaveChangesAsync();
            return product.Entity;
        }

        public async Task<Sku> AddProductToStockAsync(Sku sku, List<ushort> attributeOptions)
        {
            sku.RemainingQuantity = sku.Quantity;
            sku.IsOnSale = false;
            var newSku = _dbContext.Skus.Add(sku);

            if (!attributeOptions.IsNullOrEmpty())
            {
                var skuAttributes = attributeOptions
                    .Select(attributeOption => new SkuAttribute
                        {Sku = newSku.Entity, AttributeOptionId = attributeOption}).ToList();
                await _dbContext.SkuAttributes.AddRangeAsync(skuAttributes);
            }

            await _dbContext.SaveChangesAsync();
            return newSku.Entity;
        }

        public async Task<bool> HasAttributes(uint productId, List<ushort> attributes)
        {
            foreach (var attribute in attributes)
            {
                if (!await _dbContext.ProductAttributes.AnyAsync(pa =>
                    pa.ProductId.Equals(productId) && pa.AttributeId.Equals(attribute)))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
