using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using ShopperAdmin.Database;
using ShopperAdmin.Extensions.Helpers;
using Shared.Mvc.Entities;
using Shared.Mvc.ViewModels;
using ShopperAdmin.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shared.Extensions.Helpers;
using Shared.Mvc.Entities.Identity;

namespace ShopperAdmin.Services.Implementations
{
    public class RoleService : IRoleService
    {
        private readonly ApplicationDbContext _dbContext;

        public RoleService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Role> GetAllRoles()
        {
            return _dbContext.Roles.AsNoTracking();
        }

        public async Task<Role> FindByNameAsync(string name)
        {
            return await _dbContext.Roles.FirstOrDefaultAsync(r =>
                string.Equals(r.Name, name, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<Role> FindByIdAsync(long id)
        {
            return await _dbContext.Roles.FindAsync(id);
        }

        public async Task<Role> FindByDisplayName(string displayName)
        {
            return await _dbContext
                .Roles
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(r =>
                    string.Equals(r.DisplayName, displayName, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<Role> CreateAsync(Role role)
        {
            if (WasDeleted(role))
            {
                var deleted = await FindByNameAsync(role.Name);
                deleted.IsDeleted = false;
                await _dbContext.SaveChangesAsync();
                return deleted;
            }

            var newRole = await _dbContext.Roles.AddAsync(role);
            await _dbContext.SaveChangesAsync();
            return newRole.Entity;
        }

        public async Task<Role> UpdateAsync(Role role)
        {
            var toUpdate = await _dbContext.Roles.FindAsync(role.Id);
            toUpdate.Name = role.Name;
            var updated = _dbContext.Roles.Update(toUpdate);
            await _dbContext.SaveChangesAsync();
            return updated.Entity;
        }

        public async Task DeleteRoleAsync(Role role)
        {
            role.IsDeleted = true;
            _dbContext.Roles.Update(role);
            await _dbContext.SaveChangesAsync();
        }

        public RolePermissionViewModel RolePermissions(long roleId)
        {
            var role = _dbContext.Roles.Where(role => role.Id == roleId).Select(role => new {role.Name, role.Id})
                .First();
            var modules = _dbContext
                .Modules
                .AsNoTracking()
                .Include(module => module.Permissions)
                .ToList();
            var roleClaims = _dbContext.RoleClaims.Where(claim => claim.RoleId == roleId)
                .Select(claim => claim.ClaimValue).ToList();

            return new RolePermissionViewModel
            {
                RoleId = role.Id,
                RoleName = role.Name,
                Modules = modules,
                RoleClaims = roleClaims
            };
        }

        public async Task SaveRolePermissionsAsync(Role role, List<string> permissions)
        {
            await _dbContext.Entry(role).Collection(r => r.RoleClaims).LoadAsync();
            List<string> added;
            if (role.RoleClaims.IsNotNull())
            {
                var currentRolePermissions = role.RoleClaims.Select(claim => claim.ClaimValue).ToList();

                var deleted = role.RoleClaims
                    .Where(claim => currentRolePermissions.Except(permissions).Contains(claim.ClaimValue)).ToList();
                added = permissions.Except(currentRolePermissions).ToList();
                _dbContext.RoleClaims.RemoveRange(deleted);
            }
            else
            {
                added = permissions;
            }

            added.ForEach(s =>
            {
                if (!_dbContext.RoleClaims.Any(claim => claim.ClaimValue == s && claim.RoleId == role.Id))
                {
                    _dbContext.RoleClaims.Add(new RoleClaim
                    {
                        RoleId = role.Id,
                        ClaimType = "permission",
                        ClaimValue = s
                    });
                }
            });
            await _dbContext.SaveChangesAsync();
        }

        public bool ExistsByName(string name, bool includeDeleted)
        {
            if (includeDeleted)
            {
                return _dbContext.Roles
                    .IgnoreQueryFilters()
                    .AsNoTracking()
                    .Any(r => string.Equals(r.Name, name, StringComparison.OrdinalIgnoreCase));
            }

            return _dbContext.Roles
                .AsNoTracking()
                .Any(r => string.Equals(r.Name, name, StringComparison.OrdinalIgnoreCase));
        }

        public bool ExistsByDisplayName(string name, bool includeDeleted)
        {
            if (includeDeleted)
            {
                return _dbContext.Roles
                    .IgnoreQueryFilters()
                    .AsNoTracking()
                    .Any(r => string.Equals(r.DisplayName, name, StringComparison.OrdinalIgnoreCase));
            }

            return _dbContext.Roles
                .AsNoTracking()
                .Any(r => string.Equals(r.DisplayName, name, StringComparison.OrdinalIgnoreCase));
        }

        public bool ExistsById(long id, bool includeDeleted)
        {
            if (includeDeleted)
            {
                return _dbContext.Roles
                    .IgnoreQueryFilters()
                    .AsNoTracking()
                    .Any(r => r.Id == id);
            }

            return _dbContext.Roles
                .AsNoTracking()
                .Any(r => r.Id == id);
        }

        public bool WasDeleted(Role role)
        {
            return _dbContext.Roles
                .IgnoreQueryFilters()
                .AsNoTracking()
                .Any(r => string.Equals(r.Name, role.Name, StringComparison.OrdinalIgnoreCase)
                          && r.IsDeleted);
        }

        public string GenerateRoleName(string roleName)
        {
            roleName = roleName.Replace(" ", "-");
            var existingCount = _dbContext.Roles.Count(role => role.Name.Contains(roleName));
            return existingCount == 0 ? roleName : $"{roleName}{existingCount}";
        }
    }
}