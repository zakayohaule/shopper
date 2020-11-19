using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shared.Mvc.Entities;
using Shopper.Database;
using Shopper.Services.Interfaces;

namespace Shopper.Services.Implementations
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductCategoryService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<ProductCategory> GetAllProductCategories()
        {
            return _dbContext.ProductCategories.AsQueryable();
        }

        public async Task<ProductCategory> FindByNameAsync(string name)
        {
            return await _dbContext.ProductCategories.Where(pg => pg.Name.Equals(name)).FirstOrDefaultAsync();
        }

        public async Task<ProductCategory> FindByIdAsync(ushort id)
        {
            return await _dbContext.ProductCategories.FindAsync(id);
        }

        public async Task<ProductCategory> CreateAsync(ProductCategory productCategory)
        {
            var addedProductGroup = await _dbContext.ProductCategories.AddAsync(productCategory);
            await _dbContext.SaveChangesAsync();
            return addedProductGroup.Entity;
        }

        public async Task<ProductCategory> UpdateAsync(ProductCategory productCategory)
        {
            var updated = _dbContext.ProductCategories.Update(productCategory);
            await _dbContext.SaveChangesAsync();
            return updated.Entity;
        }

        public async Task DeleteProductCategoryAsync(ProductCategory productCategory)
        {
            _dbContext.Remove(productCategory);
            await _dbContext.SaveChangesAsync();
        }

        public bool IsDuplicate(string name, ushort id)
        {
            return _dbContext.ProductCategories.Any(pg => pg.Name.Equals(name, StringComparison.OrdinalIgnoreCase) && pg.Id != id);
        }
    }
}
