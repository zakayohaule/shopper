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
    public class ExpenditureTypeService : IExpenditureTypeService
    {
        private readonly ApplicationDbContext _dbContext;

        public ExpenditureTypeService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<ExpenditureType> GetAllExpenditureTypes()
        {
            return _dbContext.ExpenditureTypes.AsQueryable();
        }

        public List<SelectListItem> GetExpenditureTypeSelectListItems()
        {
            return GetAllExpenditureTypes()
                .Select(pg => new SelectListItem {Value = pg.Id.ToString(), Text = pg.Name}).ToList();
        }

        public async Task<ExpenditureType> FindByNameAsync(string name)
        {
            return await _dbContext.ExpenditureTypes.Where(pg => pg.Name.Equals(name)).FirstOrDefaultAsync();
        }

        public async Task<ExpenditureType> FindByIdAsync(ushort id)
        {
            return await _dbContext.ExpenditureTypes.FindAsync(id);
        }

        public async Task<ExpenditureType> CreateAsync(ExpenditureType expenditureType)
        {
            var addedExpenditureType = await _dbContext.ExpenditureTypes.AddAsync(expenditureType);
            await _dbContext.SaveChangesAsync();
            return addedExpenditureType.Entity;
        }

        public async Task<ExpenditureType> UpdateAsync(ExpenditureType expenditureType)
        {
            var updated = _dbContext.ExpenditureTypes.Update(expenditureType);
            await _dbContext.SaveChangesAsync();
            return updated.Entity;
        }

        public async Task DeleteExpenditureTypeAsync(ExpenditureType expenditureType)
        {
            _dbContext.Remove(expenditureType);
            await _dbContext.SaveChangesAsync();
        }

        public bool IsDuplicate(ExpenditureType expenditureType)
        {
            return _dbContext.ExpenditureTypes.Any(pg => pg.Name.Equals(expenditureType.Name, StringComparison.OrdinalIgnoreCase) && pg.Id != expenditureType.Id);
        }

        public bool IsDuplicate(string name, ushort id)
        {
            return _dbContext.ExpenditureTypes.Any(pg => pg.Name.Equals(name, StringComparison.OrdinalIgnoreCase) && pg.Id != id);
        }
    }
}
