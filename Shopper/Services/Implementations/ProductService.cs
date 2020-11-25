using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shared.Mvc.Entities;
using Shopper.Database;
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

        public List<SelectListItem> GetAttributeGroupings()
        {
            var attributes = _dbContext.Attributes.Include(att => att.AttributeOptions).ToList();
            var groups = new List<SelectListGroup>();
            var selectItems = new List<SelectListItem>();
            foreach (var att in attributes)
            {
                var group = new SelectListGroup();
                group.Name = att.Name;
                group.Disabled = false;
                foreach (var opt in att.AttributeOptions)
                {
                    selectItems.Add(new SelectListItem
                    {
                        Group = group,
                        Text = opt.Name,
                        Value = opt.Id.ToString()
                    });
                }

                groups.Add(group);
            }

            return selectItems;
        }

        public bool IsDuplicate(Product product)
        {
            return _dbContext
                .Products
                .Any(pg => pg.Name.Equals(product.Name, StringComparison.OrdinalIgnoreCase) &&
                           pg.Id != product.Id);
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
    }
}
