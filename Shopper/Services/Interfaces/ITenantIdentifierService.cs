using Shopper.Mvc.Entities;

namespace Shopper.Services.Interfaces
{
    public interface ITenantIdentifierService
    {
        string GetCurrentTenantConnectionString();
        public Tenant GetTenantFromRequest();
    }
}
