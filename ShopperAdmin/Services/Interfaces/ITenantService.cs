using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ShopperAdmin.Mvc.Entities;
using ShopperAdmin.Mvc.ViewModels;

namespace ShopperAdmin.Services.Interfaces
{
    public interface ITenantService
    {
        Task<Tenant> FindByIdAsync(Guid id);
        IQueryable<Tenant> GetTenantAsQueryable();
        Task<Tenant> CreateAsync(CreateTenantModel formModel);
        Task DeleteAsync(Tenant tenant);
        Task<HttpClient> GetTenantAppClientAsync(Tenant tenant);
        Task<bool> CreateTenantUserAndRole(Tenant tenant, CreateTenantModel formModel);
    }
}
