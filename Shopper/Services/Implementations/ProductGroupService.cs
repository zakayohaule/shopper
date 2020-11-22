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
    public class ProductGroupService : IProductGroupService
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductGroupService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<ProductGroup> GetAllProductGroups()
        {
            return _dbContext.ProductGroups.AsQueryable();
        }

        public List<SelectListItem> GetProductGroupSelectListItems()
        {
            return GetAllProductGroups()
                .Select(pg => new SelectListItem {Value = pg.Id.ToString(), Text = pg.Name}).ToList();
        }

        public async Task<ProductGroup> FindByNameAsync(string name)
        {
            return await _dbContext.ProductGroups.Where(pg => pg.Name.Equals(name)).FirstOrDefaultAsync();
        }

        public async Task<ProductGroup> FindByIdAsync(ushort id)
        {
            return await _dbContext.ProductGroups.FindAsync(id);
        }

        public async Task<ProductGroup> CreateAsync(ProductGroup productGroup)
        {
            var addedProductGroup = await _dbContext.ProductGroups.AddAsync(productGroup);
            await _dbContext.SaveChangesAsync();
            return addedProductGroup.Entity;
        }

        public async Task<ProductGroup> UpdateAsync(ProductGroup productGroup)
        {
            var updated = _dbContext.ProductGroups.Update(productGroup);
            await _dbContext.SaveChangesAsync();
            return updated.Entity;
        }

        public async Task DeleteProductGroupAsync(ProductGroup productGroup)
        {
            _dbContext.Remove(productGroup);
            await _dbContext.SaveChangesAsync();
        }

        public bool IsDuplicate(ProductGroup productGrpGroup)
        {
            return _dbContext.ProductGroups.Any(pg => pg.Name.Equals(productGrpGroup.Name, StringComparison.OrdinalIgnoreCase) && pg.Id != productGrpGroup.Id);
        }

        public bool IsDuplicate(string name, ushort id)
        {
            return _dbContext.ProductGroups.Any(pg => pg.Name.Equals(name, StringComparison.OrdinalIgnoreCase) && pg.Id != id);
        }
    }
}
