using System;
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

        public static char NextAlphabet(this Random rnd)
        {
            int rndIndex = rnd.Next(0, 26);
            return (char)('a' + rndIndex);
        }

        public static string NextAlphabets(this Random rnd, short numberOfChars)
        {
            var chars = "";
            for (int i = 0; i < numberOfChars; i++)
            {
                int rndIndex = rnd.Next(0, 26);
                chars += (char)('a' + rndIndex);
            }

            return chars.ToUpper();
        }

        public static string NextAlphabets(this string chars, short numberOfChars)
        {
            Random random = new Random();
            chars = random.NextAlphabets(numberOfChars);
            return chars;
        }

        public static string SuggestTenantCode(this string code)
        {
            Random random = new Random();
            var numb = random.Next(0, 10);
            var alph = random.NextAlphabets(3);
            return alph+numb;
        }
    }
}
