using Microsoft.AspNetCore.Http;
using Shopper.Common;
using Shopper.Extensions.Helpers;
using Shopper.Mvc.Entities;
using Shopper.Services.Interfaces;

namespace Shopper.Services.Implementations
{
    public class TenantIdentifierService : ITenantIdentifierService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TenantIdentifierService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetCurrentTenantConnectionString()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var tenant = httpContext?.GetCurrentTenant();
            return tenant?.ConnectionString;
        }

        public Tenant GetTenantFromRequest()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var tenant = httpContext?.GetCurrentTenant();
            if (tenant == null)
            {
                return new Tenant
                {

                };
            }

            return tenant;
        }

        public Tenant GetTenantFromRequestOrDefault()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var tenant = httpContext.GetCurrentTenant();
            if (tenant == null)
            {
                throw new InvalidTenantException("Invalid tenant");
            }

            return null;
        }

    }
}
