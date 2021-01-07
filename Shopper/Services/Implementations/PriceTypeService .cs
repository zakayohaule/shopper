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
    public class PriceTypeService : IPriceTypeService
    {
        private readonly ApplicationDbContext _dbContext;

        public PriceTypeService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<PriceType> GetAllPriceTypes()
        {
            return _dbContext.PriceTypes.AsQueryable().AsNoTracking();
        }

        public List<SelectListItem> GetPriceTypeSelectListItems()
        {
            return GetAllPriceTypes()
                .Select(pg => new SelectListItem {Value = pg.Id.ToString(), Text = pg.Name}).ToList();
        }

        public async Task<PriceType> FindByNameAsync(string name)
        {
            return await _dbContext.PriceTypes.Where(pg => pg.Name.Equals(name)).FirstOrDefaultAsync();
        }

        public async Task<PriceType> FindByIdAsync(ushort id)
        {
            return await _dbContext.PriceTypes.FindAsync(id);
        }

        public async Task<PriceType> CreateAsync(PriceType priceType)
        {
            var addedPriceType = await _dbContext.PriceTypes.AddAsync(priceType);
            await _dbContext.SaveChangesAsync();
            return addedPriceType.Entity;
        }

        public async Task<PriceType> UpdateAsync(PriceType priceType)
        {
            var updated = _dbContext.PriceTypes.Update(priceType);
            await _dbContext.SaveChangesAsync();
            return updated.Entity;
        }

        public async Task DeletePriceTypeAsync(PriceType priceType)
        {
            _dbContext.Remove(priceType);
            await _dbContext.SaveChangesAsync();
        }

        public bool IsDuplicate(PriceType priceType)
        {
            return _dbContext.PriceTypes.Any(pg => pg.Name.Equals(priceType.Name, StringComparison.OrdinalIgnoreCase) && pg.Id != priceType.Id);
        }

        public bool IsDuplicate(string name, ushort id)
        {
            return _dbContext.PriceTypes.Any(pg => pg.Name.Equals(name, StringComparison.OrdinalIgnoreCase) && pg.Id != id);
        }
    }
}
