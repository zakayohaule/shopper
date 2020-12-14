using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shared.Mvc.Entities;
using Shopper.Database;
using Shopper.Services.Interfaces;

namespace Shopper.Services.Implementations
{
    public class AttributeOptionService : IAttributeOptionService
    {
        private readonly ApplicationDbContext _dbContext;

        public AttributeOptionService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<AttributeOption> GetAllAttributeOptions()
        {
            return _dbContext.AttributeOptions.AsQueryable();
        }

        public IQueryable<AttributeOption> GetAllAttributeOptionsByAttribute(ushort attributeId)
        {
            return GetAllAttributeOptions().Where(ao => ao.AttributeId.Equals(attributeId));
        }

        public async Task<AttributeOption> FindByNameAsync(string name)
        {
            return await _dbContext.AttributeOptions.Where(pg => pg.Name.Equals(name)).FirstOrDefaultAsync();
        }

        public async Task<AttributeOption> FindByIdAsync(ushort id)
        {
            return await _dbContext.AttributeOptions.FindAsync(id);
        }

        public async Task<AttributeOption> CreateAsync(AttributeOption productCategory)
        {
            var addedProductGroup = await _dbContext.AttributeOptions.AddAsync(productCategory);
            await _dbContext.SaveChangesAsync();
            return addedProductGroup.Entity;
        }

        public async Task<AttributeOption> UpdateAsync(AttributeOption productCategory)
        {
            var updated = _dbContext.AttributeOptions.Update(productCategory);
            await _dbContext.SaveChangesAsync();
            return updated.Entity;
        }

        public async Task DeleteAttributeOptionAsync(AttributeOption productCategory)
        {
            _dbContext.Remove(productCategory);
            await _dbContext.SaveChangesAsync();
        }

        public bool IsDuplicate(AttributeOption productCategory)
        {
            return _dbContext
                .AttributeOptions
                .Any(pg => pg.Name.Equals(productCategory.Name, StringComparison.OrdinalIgnoreCase) &&
                           pg.Id != productCategory.Id);
        }

        public bool IsDuplicate(string name, ushort id)
        {
            return _dbContext
                .AttributeOptions
                .Any(pg => pg.Name.Equals(name, StringComparison.OrdinalIgnoreCase) &&
                           pg.Id != id);
        }

        public async Task<bool> ExistsByIdAsync(ushort id)
        {
            return await _dbContext.AttributeOptions.AnyAsync(pc => pc.Id.Equals(id));
        }
    }
}
