using System;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Shared.Mvc.Entities;
using Shopper.Database;

namespace Shopper.Services
{
    public class TenantResolver
    {
        private readonly IMemoryCache _memoryCache;

        public TenantResolver(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public Tenant Resolve(HttpContext httpContext)
        {
            var domain = httpContext.Request.Host.Host;
            if(_memoryCache.TryGetValue($"tenant_{domain}", out Tenant tenant)) return tenant;
            var dbContext = httpContext.RequestServices.GetRequiredService<AdminAppDbContext>();
            var resolvedTenant = dbContext.Tenants.FirstOrDefault(t => t.Domain.Equals(domain));
            if (resolvedTenant != null)
            {
                _memoryCache.Set($"tenant_{resolvedTenant.Domain}", resolvedTenant, new MemoryCacheEntryOptions
                {
                    SlidingExpiration = TimeSpan.FromMinutes(120)
                });
                return resolvedTenant;
            }

            return null;
        }

    }
}
