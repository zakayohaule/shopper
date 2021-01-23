using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shopper.Database;
using Shopper.Mvc.Entities;
using Shopper.Services.Interfaces;

namespace Shopper.Services.Implementations
{
    public class ExpenditureService : IExpenditureService
    {
        private readonly ApplicationDbContext _dbContext;

        public ExpenditureService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Expenditure> GetExpenditureAsQuerable()
        {
            return _dbContext.Expenditures.AsQueryable();
        }

        public async Task<Expenditure> FindByIdAsync(ulong id)
        {
            return await _dbContext.Expenditures.FindAsync(id);
        }

        public async Task<Expenditure> CreateAsync(Expenditure expenditure)
        {
            var addedProductGroup = await _dbContext.Expenditures.AddAsync(expenditure);
            await _dbContext.SaveChangesAsync();
            return addedProductGroup.Entity;
        }

        public async Task<Expenditure> UpdateAsync(Expenditure expenditure)
        {
            var updated = _dbContext.Expenditures.Update(expenditure);
            await _dbContext.SaveChangesAsync();
            return updated.Entity;
        }

        public async Task DeleteExpenditureAsync(Expenditure expenditure)
        {
            _dbContext.Remove(expenditure);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> ExistsByIdAsync(ulong id)
        {
            return await _dbContext.Expenditures.AnyAsync(pc => pc.Id.Equals(id));
        }
    }
}
