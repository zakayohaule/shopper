using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shared.Extensions.Helpers;
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

        async Task<Product> IProductService.FindByIdAsync(uint id)
        {
            return await _dbContext.Products.FindAsync(id);
        }

        public IQueryable<Product> FindByIdAsyncQ(uint id)
        {
            return _dbContext.Products.Where(product => product.Id.Equals(id)).AsQueryable();
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

        public async Task<Product> CreateProductAsync(ProductFormModel newProduct)
        {
            var product = await _dbContext.Products.AddAsync(new Product{Name = newProduct.Name, ProductCategoryId = newProduct.ProductCategoryId});

            if (newProduct.Attributes.Any())
            {
                var productAttributes = newProduct.Attributes.Select(attribute => new ProductAttribute {AttributeId = attribute, Product = product.Entity,}).ToList();

                await _dbContext.ProductAttributes.AddRangeAsync(productAttributes);
            }

            await _dbContext.SaveChangesAsync();
            return product.Entity;
        }

        public async Task<Product> UpdateProductAsync(ProductFormModel productModel, Product productToUpdate)
        {
            productToUpdate.Name = productModel.Name;
            productToUpdate.ProductCategoryId = productModel.ProductCategoryId;
            // productToUpdate.Attributes.Clear();
            _dbContext.ProductAttributes.RemoveRange(productToUpdate.Attributes);
            if (productModel.Attributes.IsNotNull() && productModel.Attributes.Any())
            {
                var productAttributes = productModel.Attributes.Select(attribute => new ProductAttribute {AttributeId = attribute, Product = productToUpdate,}).ToList();

                await _dbContext.ProductAttributes.AddRangeAsync(productAttributes);
            }

            var prod = _dbContext.Products.Update(productToUpdate);
            await _dbContext.SaveChangesAsync();
            return prod.Entity;
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

        public async Task DeleteProductAsync(Product productGroup)
        {
            _dbContext.Remove(productGroup);
            await _dbContext.SaveChangesAsync();
        }
    }
}
