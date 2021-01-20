using System.Collections.Generic;
using System.Threading.Tasks;
using ShopperAdmin.Mvc.Entities.Identity;
using ShopperAdmin.Mvc.ViewModels;

namespace ShopperAdmin.Services.Interfaces
{
    public interface IRoleService
    {
        IEnumerable<Role> GetAllRoles();
        Task<Role> FindByNameAsync(string name);
        Task<Role> FindByIdAsync(long id);
        Task<Role> FindByDisplayName(string displayName);
        Task<Role> CreateAsync(Role role);
        Task<Role> UpdateAsync(Role role);
        Task DeleteRoleAsync(Role role);
        RolePermissionViewModel RolePermissions(long roleId);
        Task SaveRolePermissionsAsync(Role role, List<string> permissions);

        string GenerateRoleName(string roleName);
        bool ExistsByName(string name, bool includeDeleted);
        bool ExistsByDisplayName(string name);
        bool ExistsById(long id, bool includeDeleted);
        bool WasDeleted(Role role);
    }
}
