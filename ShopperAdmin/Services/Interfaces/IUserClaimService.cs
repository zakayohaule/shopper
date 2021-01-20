using System.Collections.Generic;

namespace ShopperAdmin.Services.Interfaces
{
    public interface IUserClaimService
    {
        List<string> GetUserClaims(long userId);
        void CacheClaims(long userId, List<string> claims);
        bool HasPermission(long userId, string permission);
        void RemoveClaims(long userId);
    }
}