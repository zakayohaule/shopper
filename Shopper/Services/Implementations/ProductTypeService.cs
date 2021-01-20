using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shopper.Database;
using Shopper.Mvc.Entities;
using Shopper.Services.Interfaces;

namespace Shopper.Services.Implementations
{
    public class ProductTypeService : IProductTypeService
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductTypeService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<ProductType> GetAllProductTypes()
        {
            return _dbContext.ProductTypes.AsQueryable();
        }

        public List<SelectListItem> GetProductTypeSelectListItems()
        {
            return GetAllProductTypes()
                .Include(pt => pt.ProductCategory)
                .Select(pt => new SelectListItem {Value = pt.Id.ToString(), Text = $"{pt.ProductCategory.Name} - {pt.Name}"}).ToList();
        }

        public List<SelectListItem> GetProductTypeSelectListItemsByCategoryId(ushort categoryId, ushort? selectedTypeId = null)
        {
            return GetAllProductTypes()
                .AsNoTracking()
                .Where(pt => pt.ProductCategoryId.Equals(categoryId))
                .Select(pt => new SelectListItem
                {
                    Value = pt.Id.ToString(),
                    Text = pt.Name,
                    Selected = pt.Id.Equals(selectedTypeId)
                }).ToList();
        }

        public async Task<ProductType> FindByNameAsync(string name)
        {
            return await _dbContext.ProductTypes.Where(pt => pt.Name.Equals(name)).FirstOrDefaultAsync();
        }

        public async Task<ProductType> FindByIdAsync(ushort id)
        {
            return await _dbContext.ProductTypes.FindAsync(id);
        }

        public async Task<ProductType> CreateAsync(ProductType productType)
        {
            var addedProductGroup = await _dbContext.ProductTypes.AddAsync(productType);
            await _dbContext.SaveChangesAsync();
            return addedProductGroup.Entity;
        }

        public async Task<ProductType> UpdateAsync(ProductType productType)
        {
            var updated = _dbContext.ProductTypes.Update(productType);
            await _dbContext.SaveChangesAsync();
            return updated.Entity;
        }

        public async Task DeleteProductTypeAsync(ProductType productType)
        {
            _dbContext.Remove(productType);
            await _dbContext.SaveChangesAsync();
        }

        public bool IsDuplicate(ProductType productType)
        {
            return _dbContext
                .ProductTypes
                .Any(pt => pt.Name.Equals(productType.Name, StringComparison.OrdinalIgnoreCase) &&
                           pt.Id != productType.Id);
        }

        public bool IsDuplicate(string name, ushort id)
        {
            return _dbContext
                .ProductTypes
                .Any(pt => pt.Name.Equals(name, StringComparison.OrdinalIgnoreCase) &&
                           pt.Id != id);
        }

        public async Task<bool> ExistsByIdAsync(ushort id)
        {
            return await _dbContext.ProductTypes.AnyAsync(pc => pc.Id.Equals(id));
        }
    }
}
