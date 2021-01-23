using System.Collections.Generic;
using IdentityServer4.Models;

namespace Shopper.Other
{
    public class IdentityConfig
    {
        public static IEnumerable<ApiResource> Apis =>
            new List<ApiResource>
            {
                new ApiResource("add_user", "Add Tenant User")
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "shopper_admin",
                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("@Zakayo0810".Sha256())
                    },
                    // scopes that client has access to
                    AllowedScopes = {"add_user"}
                }
            };
    }
}
