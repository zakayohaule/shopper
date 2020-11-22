using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shopper.Database;
using Shopper.Services.Interfaces;
using Attribute = Shared.Mvc.Entities.Attribute;

namespace Shopper.Services.Implementations
{
    public class AttributeService : IAttributeService
    {
        private readonly ApplicationDbContext _dbContext;

        public AttributeService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Attribute> GetAllAttributes()
        {
            return _dbContext.Attributes.AsQueryable();
        }

        public async Task<Attribute> FindByNameAsync(string name)
        {
            return await _dbContext.Attributes.Where(pg => pg.Name.Equals(name)).FirstOrDefaultAsync();
        }

        public async Task<Attribute> FindByIdAsync(ushort id)
        {
            return await _dbContext.Attributes.FindAsync(id);
        }

        public async Task<Attribute> CreateAsync(Attribute attribute)
        {
            var addedAttribute = await _dbContext.Attributes.AddAsync(attribute);
            await _dbContext.SaveChangesAsync();
            return addedAttribute.Entity;
        }

        public async Task<Attribute> UpdateAsync(Attribute attribute)
        {
            var updated = _dbContext.Attributes.Update(attribute);
            await _dbContext.SaveChangesAsync();
            return updated.Entity;
        }

        public async Task DeleteAttributeAsync(Attribute attribute)
        {
            _dbContext.Remove(attribute);
            await _dbContext.SaveChangesAsync();
        }

        public bool IsDuplicate(Attribute attribute)
        {
            return _dbContext.Attributes.Any(pg => pg.Name.Equals(attribute.Name, StringComparison.OrdinalIgnoreCase) && pg.Id != attribute.Id);
        }

        public bool IsDuplicate(string name, ushort id)
        {
            return _dbContext.Attributes.Any(pg => pg.Name.Equals(name, StringComparison.OrdinalIgnoreCase) && pg.Id != id);
        }
    }
}
