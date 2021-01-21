using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopperAdmin.Services.Interfaces
{
    public interface IUserClaimService
    {
        List<string> GetUserClaims(long userId);
        void CacheClaims(long userId, List<string> claims);
        bool HasPermission(long userId, string permission);
        bool HasAllPermissions(long userId, string permissions);
        bool HasAllPermissions(long userId, IEnumerable<string> permissions);
        bool HasAnyPermission(long userId, string permissions);
        bool HasAnyPermission(long userId, IEnumerable<string> permissions);
        void RemoveClaims(long userId);
        Task ReCacheUsersRoleClaims(long roleId);
    }
}
