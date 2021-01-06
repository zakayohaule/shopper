namespace Shopper.Services.Interfaces
{
    public interface ITenantService
    {
        public string GenerateTenantCode(string tenantName);
    }
}
