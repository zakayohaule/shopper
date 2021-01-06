using System;
using System.Collections.Generic;
using System.Linq;
using IdentityServer4.Extensions;
using JetBrains.Annotations;
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

        public static string NextAlphabets([NotNull] this string chars, short numberOfChars)
        {
            if (chars == null) throw new ArgumentNullException(nameof(chars));
            Random random = new Random();
            chars = random.NextAlphabets(numberOfChars);
            return chars;
        }

        public static List<string> PossibleCombinations([NotNull] this string str, int listCount = 10)
        {
            var combinations = new List<string>();
            if (str == null) throw new ArgumentNullException(nameof(str));
            var name = str.Split(" ").FirstOrDefault();
            if (name.IsNullOrEmpty())
            {
                combinations.Add(str.NextAlphabets(3));
                return combinations;
            }

            while (combinations.Count < listCount)
            {
                combinations.Add(str.RandomCharAsStringFromString(3).ToUpper());
            }
            return combinations;
        }

        public static char RandomCharFromString(this string randomString)
        {
            randomString = randomString.ToUpper();
            var random = new Random();
            return randomString[random.Next(0, randomString.Length)];
        }

        public static string RandomCharAsStringFromString(this string str, int length)
        {
            var randString = "";
            for (var i = 0; i < length; i++)
            {
                randString += str.RandomCharFromString();
            }
            return randString.ToUpper();
        }

        public static string SuggestTenantCode(this string code)
        {
            Random random = new Random();
            // var numb = random.Next(0, 10);
            var alph = random.NextAlphabets(3);
            return alph;
        }
    }
}
