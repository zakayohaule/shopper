using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Serilog;
using ShopperAdmin.Extensions.Helpers;
using ShopperAdmin.Mvc.Entities.Identity;
using ShopperAdmin.Services.Interfaces;

namespace ShopperAdmin.Services.Implementations
{
    public class UserClaimService : IUserClaimService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IConfiguration _configuration;

        private readonly ILogger _logger;
        // private readonly string _prefix = "PERMISSION";

        public UserClaimService(IMemoryCache memoryCache,
            UserManager<AppUser> userManager,
            RoleManager<Role> roleManager,
            IConfiguration configuration, ILogger logger)
        {
            _memoryCache = memoryCache;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _logger = logger;
        }


        public void CacheClaims(long userId, List<string> claims)
        {
            var expirationTime = _configuration.GetSection("Cookie").GetValue<int?>("ExpiryMinutes") ?? 60;

            _memoryCache.Set(userId, claims, new MemoryCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromMinutes(expirationTime)
            });
        }

        public List<string> GetUserClaims(long userId)
        {
            var claims = new List<string>();
            var user = _userManager.FindByIdAsync(userId.ToString()).Result;
            if (user == null)
            {
                return claims;
            }
            var userRoles = _userManager.GetRolesAsync(user).Result.ToList();

            userRoles.ForEach(roleName =>
            {
                Console.WriteLine(roleName);
                var role = _roleManager.FindByNameAsync(roleName).Result;
                var roleClaims = _roleManager.GetClaimsAsync(role).Result;
                claims.AddRange(roleClaims.Select(claim => claim.Value).ToList());
            });

            return claims;
        }

        public bool HasPermission(long userId, string permission)
        {
            if (_memoryCache.TryGetValue(userId, out List<string> userClaims)) return userClaims.Contains(permission);
            _logger.Information("******** re-caching user role claims");
            userClaims = GetUserClaims(userId);
            CacheClaims(userId, userClaims);
            return userClaims.Contains(permission);
        }

        public void RemoveClaims(long userId)
        {
            _logger.Information("Removing user claims from memory cache before signing out user");
            _memoryCache.Remove(userId);
        }
    }
}
