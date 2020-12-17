using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Serilog;
using Shared.Extensions.Helpers;
using Shared.Mvc.Entities.Identity;
using Shopper.Database;
using Shopper.Services.Interfaces;

namespace Shopper.Services.Implementations
{
    [Authorize]
    public class UserClaimService : IUserClaimService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _dbContext;

        private readonly ILogger _logger;
        // private readonly string _prefix = "PERMISSION";

        public UserClaimService(IMemoryCache memoryCache,
            UserManager<AppUser> userManager,
            RoleManager<Role> roleManager,
            IConfiguration configuration, ILogger logger, ApplicationDbContext dbContext)
        {
            _memoryCache = memoryCache;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _logger = logger;
            _dbContext = dbContext;
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
            var user = _dbContext.Users.Find(userId);
            if (user.IsNull())
            {
                return claims;
            }

            var userRoles = _userManager.GetRolesAsync(user).Result.ToList();

            userRoles.ForEach(roleName =>
            {
                Console.WriteLine(roleName);
                // var role = _roleManager.FindByNameAsync(roleName).Result;
                var role = _dbContext.Roles.FirstOrDefault(r => r.Name.Equals(roleName) && r.TenantId == user.TenantId);
                var roleClaims = _roleManager.GetClaimsAsync(role).Result;
                claims.AddRange(roleClaims.Select(claim => claim.Value).ToList());
            });

            return claims;
        }

        public bool HasPermission(long userId, string permission)
        {
            if (_memoryCache.TryGetValue(userId, out List<string> userClaims))
            {
                return userClaims.Contains(permission);
            }

            _logger.Information("******** re-caching user role claims");
            userClaims = GetUserClaims(userId);
            CacheClaims(userId, userClaims);
            return userClaims.Contains(permission);
        }

        public bool HasAllPermissions(long userId, string permissions)
        {
            var permissionList = permissions.Split(",", StringSplitOptions.RemoveEmptyEntries);
            return permissionList.All(perm => HasPermission(userId, perm));
        }

        public bool HasAnyPermission(long userId, string permissions)
        {
            var permissionList = permissions.Split(",", StringSplitOptions.RemoveEmptyEntries);
            return permissionList.Any(perm => HasPermission(userId, perm));
        }

        public bool HasAllPermissions(long userId, IEnumerable<string> permissions)
        {
            return permissions.All(perm => HasPermission(userId, perm));
        }

        public bool HasAnyPermission(long userId, IEnumerable<string> permissions)
        {
            return permissions.Any(perm => HasPermission(userId, perm));
        }

        public void RemoveClaims(long userId)
        {
            _logger.Information("Removing user claims from memory cache");
            _memoryCache.Remove(userId);
        }

        public async Task ReCacheUsersRoleClaims(long roleId)
        {
            var users = await _userManager.Users
                .Include(user => user.UserRoles)
                .Where(user => user.UserRoles.Any(ur => ur.RoleId.Equals(roleId)))
                .ToListAsync();
            foreach (var appUser in users)
            {
                if (!_memoryCache.TryGetValue(appUser.Id, out List<string> userClaims)) continue;
                Console.WriteLine($"Permission changed, re-caching {appUser.FullName}'s claims!");
                RemoveClaims(appUser.Id);
                userClaims = GetUserClaims(appUser.Id);
                CacheClaims(appUser.Id, userClaims);
            }
        }
    }
}
