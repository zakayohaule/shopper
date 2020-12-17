using Shared.Mvc.Entities;

namespace Shopper.Services.Interfaces
{
    public interface ITenantService
    {
        string GetCurrentTenantConnectionString();
        public Tenant GetTenantFromRequest();
    }
}
