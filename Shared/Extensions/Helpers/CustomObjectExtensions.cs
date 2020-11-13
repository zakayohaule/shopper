﻿using System.Linq;
 using System.Security.Claims;
 using Newtonsoft.Json;

 namespace Shared.Extensions.Helpers
{
    public static class CustomObjectExtensions
    {
        public static bool IsNull(this object @object)
        {
            return @object == null;
        }

        public static bool IsNotNull(this object @object)
        {
            return @object != null;
        }

        public static string Serialize<T>(this T @object)
        {
            return JsonConvert.SerializeObject(@object, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }

        public static T Deserialize<T>(this string @object)
        {
            return JsonConvert.DeserializeObject<T>(@object);
        }
        
        /// <summary>
        /// //Use CustomClaimTypes when using this method
        /// </summary>
        /// <param name="user"></param>
        /// <param name="claimType">Use [CustomClaimTypes] when using this method</param>
        /// <returns></returns>
        public static string GetUserProperty(this ClaimsPrincipal user, string claimType)
        {
            if (user.Identity.IsAuthenticated)
            {
                return user.Claims.FirstOrDefault(v => v.Type == claimType)?.Value ?? string.Empty;
            }
            return string.Empty;
        }
    }
}