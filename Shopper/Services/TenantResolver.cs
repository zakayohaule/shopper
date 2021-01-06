using System;
using System.Linq;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Common;
using Shared.Mvc.Entities;
using Shopper.Database;
using Shopper.Extensions.Helpers;

namespace Shopper.Services
{
    public class TenantResolver
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IConfiguration _configuration;

        public TenantResolver(IMemoryCache memoryCache, IConfiguration configuration)
        {
            _memoryCache = memoryCache;
            this._configuration = configuration;
        }

        public Tenant Resolve(HttpContext httpContext)
        {
            var subDomain = httpContext.GetSubdomain();
            if (subDomain.IsNullOrEmpty())
            {
                subDomain = _configuration.GetValue<string>("DefaultSubdomain");
            }

            if (_memoryCache.TryGetValue($"tenant_{subDomain}", out Tenant tenant)) return tenant;
            var dbContext = httpContext.RequestServices.GetRequiredService<AdminAppDbContext>();
            var resolvedTenant = dbContext.Tenants.FirstOrDefault(t => t.Domain.Equals(subDomain));

            if (resolvedTenant == null)
            {
                throw new InvalidTenantException($"Invalid tenant: {subDomain}");
            }

            _memoryCache.Set($"tenant_{resolvedTenant.Domain}", resolvedTenant, new MemoryCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromMinutes(120)
            });
            return resolvedTenant;
        }
    }
}
