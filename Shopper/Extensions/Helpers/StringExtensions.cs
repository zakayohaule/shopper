using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Http;

namespace Shopper.Extensions.Helpers
{
    public static class StringExtensions
    {
        public static string LoadProductImage(this string imageName, HttpContext context)
        {
            var tenant = context.GetCurrentTenant();
            if (tenant == null)
            {
                return "kea";
            }
            return $"/uploads/products/{tenant.Domain}/{imageName}";
        }

        public static string LoadProductImageThumbnail(this string imageName, HttpContext context)
        {
            var tenant = context.GetCurrentTenant();
            if (tenant == null)
            {
                return "kea";
            }
            return $"/uploads/products/thumb_{tenant.Domain}/{imageName}";
        }

        public static string LoadTenantImage(this string imageName, HttpContext context)
        {
            var tenant = context.GetCurrentTenant();
            if (tenant == null)
            {
                return "kea";
            }
            return $"/uploads/logo/{tenant.Domain}/{imageName}";
        }

        public static string LoadTenantImageThumbnail(this string imageName, HttpContext context)
        {
            var tenant = context.GetCurrentTenant();
            if (tenant == null)
            {
                return "kea";
            }
            return $"/uploads/logo/{tenant.Domain}/thumb_{imageName}";
        }
    }
}
