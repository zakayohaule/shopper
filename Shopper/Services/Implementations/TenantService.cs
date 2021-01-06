using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Shared.Common;
using Shared.Extensions.Helpers;
using Shared.Mvc.Entities;
using Shopper.Database;
using Shopper.Extensions.Helpers;
using Shopper.Services.Interfaces;

namespace Shopper.Services.Implementations
{
    public class TenantService : ITenantService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TenantService(IHttpContextAccessor httpContextAccessor)
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
