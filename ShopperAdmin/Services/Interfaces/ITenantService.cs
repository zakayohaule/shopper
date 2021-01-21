using System.Linq;
using System.Threading.Tasks;
using ShopperAdmin.Mvc.Entities;

namespace ShopperAdmin.Services.Interfaces
{
    public interface ITenantService
    {
        IQueryable<Tenant> GetTenantAsQuerable();
        Task<Tenant> FindByIdAsync(ulong id);
        Task<Tenant> CreateAsync(Tenant expenditure);
        Task<Tenant> UpdateAsync(Tenant expenditure);
        Task DeleteTenantAsync(Tenant expenditure);
        Task<bool> ExistsByIdAsync(ulong id);
    }
}
