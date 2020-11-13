using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Mvc.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

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