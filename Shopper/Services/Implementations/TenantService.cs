using System;
using System.Linq;
using IdentityServer4.Extensions;
using Shopper.Database;
using Shopper.Extensions.Helpers;
using Shopper.Services.Interfaces;

namespace Shopper.Services.Implementations
{
    public class TenantService : ITenantService
    {
        private readonly ApplicationDbContext _dbContext;

        public TenantService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public string GenerateTenantCode(string tenantName)
        {
            var firstPart = tenantName.Split(" ")[0];
            var code = "";
            if (firstPart.Length == 3)
            {
                if (!_dbContext.Tenants.Any(t => t.Code.Equals(firstPart, StringComparison.OrdinalIgnoreCase)))
                {
                    code = firstPart;
                    return code.ToUpper();
                }
            }
            if (firstPart.Length>=3)
            {
                for (int i = 0; i < firstPart.Length-2; i++)
                {
                    if (!_dbContext.Tenants.Any(t => t.Code.Equals(firstPart.Substring(i,3), StringComparison.OrdinalIgnoreCase)))
                    {
                        code = firstPart;
                        return code.ToUpper();
                    }
                }

                for (int i = 0; i < 10; i++)
                {
                    var combinations = tenantName.PossibleCombinations();
                    if (_dbContext.Tenants.Any(t => !combinations.Contains(t.Code.ToUpper())))
                    {
                        code = combinations.FirstOrDefault(comb =>
                            !_dbContext.Tenants.Any(t => t.Code.Equals(comb, StringComparison.OrdinalIgnoreCase)));
                        if (!code.IsNullOrEmpty())
                        {
                            if (code != null) return code.ToUpper();
                        }
                    }
                }
            }

            code = "".NextAlphabets(3);
            while (_dbContext.Tenants.Any(t => t.Code.Equals(code, StringComparison.OrdinalIgnoreCase)))
            {
                code = "".NextAlphabets(3);
            }

            return code;
        }
    }
}
