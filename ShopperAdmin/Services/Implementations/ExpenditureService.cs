using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopperAdmin.Database;
using ShopperAdmin.Mvc.Entities;
using ShopperAdmin.Services.Interfaces;

namespace ShopperAdmin.Services.Implementations
{
    public class TenantService : ITenantService
    {
        private readonly ApplicationDbContext _dbContext;

        public TenantService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Tenant> GetTenantAsQuerable()
        {
            return _dbContext.Tenants.AsQueryable();
        }

        public async Task<Tenant> FindByIdAsync(ulong id)
        {
            return await _dbContext.Tenants.FindAsync(id);
        }

        public async Task<Tenant> CreateAsync(Tenant tenant)
        {
            var addedProductGroup = await _dbContext.Tenants.AddAsync(tenant);
            await _dbContext.SaveChangesAsync();
            return addedProductGroup.Entity;
        }

        public async Task<Tenant> UpdateAsync(Tenant tenant)
        {
            var updated = _dbContext.Tenants.Update(tenant);
            await _dbContext.SaveChangesAsync();
            return updated.Entity;
        }

        public async Task DeleteTenantAsync(Tenant tenant)
        {
            _dbContext.Remove(tenant);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> ExistsByIdAsync(ulong id)
        {
            return await _dbContext.Tenants.AnyAsync(pc => pc.Id.Equals(id));
        }
    }
}
