using IdentityServer4.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

namespace ShopperAdmin.Extensions.Configurations
{
    public static class ModelBuilderExtensions
    {
        public static void CustomConfigureClientContext(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>(client =>
            {
                client.ToTable("clients");
                client.HasKey(x => x.Id);

                client.Property(x => x.Id).HasColumnName("id");
                client.Property(x => x.ClientId).HasMaxLength(200).IsRequired().HasColumnName("client_id");
                client.Property(x => x.ProtocolType).HasMaxLength(200).IsRequired().HasColumnName("protocol_type");
                client.Property(x => x.ClientName).HasMaxLength(200).HasColumnName("client_name");
                client.Property(x => x.ClientUri).HasMaxLength(2000).HasColumnName("client_uri");
                client.Property(x => x.LogoUri).HasMaxLength(2000).HasColumnName("logo_uri");
                client.Property(x => x.Description).HasMaxLength(1000).HasColumnName("description");
                client.Property(x => x.FrontChannelLogoutUri).HasMaxLength(2000).HasColumnName("front_channel_logout_uri");
                client.Property(x => x.FrontChannelLogoutSessionRequired).HasColumnName("front_channel_logout_session_required");
                client.Property(x => x.BackChannelLogoutUri).HasMaxLength(2000).HasColumnName("back_channel_logout_uri");
                client.Property(x => x.BackChannelLogoutSessionRequired).HasColumnName("back_channel_logout_session_required");
                client.Property(x => x.ClientClaimsPrefix).HasMaxLength(200).HasColumnName("client_claims_prefix");
                client.Property(x => x.PairWiseSubjectSalt).HasMaxLength(200).HasColumnName("pairwise_subject_salt");
                client.Property(x => x.UserCodeType).HasMaxLength(100).HasColumnName("user_code_type");
                client.Property(x => x.Enabled).HasColumnName("enabled");
                client.Property(x => x.RequireClientSecret).HasColumnName("require_client_secret");
                client.Property(x => x.RequireConsent).HasColumnName("require_consent");
                client.Property(x => x.AllowRememberConsent).HasColumnName("allow_remember_consent");
                client.Property(x => x.AlwaysIncludeUserClaimsInIdToken).HasColumnName("always_include_user_claim_in_idtoken");
                client.Property(x => x.RequirePkce).HasColumnName("require_pkce");
                client.Property(x => x.AllowPlainTextPkce).HasColumnName("allow_plain_text_pkce");
                client.Property(x => x.AllowAccessTokensViaBrowser).HasColumnName("allow_access_token_via_browser");
                client.Property(x => x.AllowOfflineAccess).HasColumnName("allow_offline_access");
                client.Property(x => x.IdentityTokenLifetime).HasColumnName("identity_token_lifetime");
                client.Property(x => x.AccessTokenLifetime).HasColumnName("access_token_lifetime");
                client.Property(x => x.AuthorizationCodeLifetime).HasColumnName("authorization_code_lifetime");
                client.Property(x => x.ConsentLifetime).HasColumnName("consent_lifetime");
                client.Property(x => x.AbsoluteRefreshTokenLifetime).HasColumnName("absolute_refresh_token_lifetime");
                client.Property(x => x.SlidingRefreshTokenLifetime).HasColumnName("sliding_refresh_token_lifetime");
                client.Property(x => x.RefreshTokenUsage).HasColumnName("refresh_token_usage");
                client.Property(x => x.UpdateAccessTokenClaimsOnRefresh).HasColumnName("update_access_token_claims_on_refresh");
                client.Property(x => x.RefreshTokenExpiration).HasColumnName("refresh_token_expiration");
                client.Property(x => x.AccessTokenType).HasColumnName("access_token_type");
                client.Property(x => x.EnableLocalLogin).HasColumnName("enable_local_login");
                client.Property(x => x.IncludeJwtId).HasColumnName("include_jwt_id");
                client.Property(x => x.AlwaysSendClientClaims).HasColumnName("always_send_client_claims");
                client.Property(x => x.Created).HasColumnName("created");
                client.Property(x => x.Updated).HasColumnName("updated");
                client.Property(x => x.LastAccessed).HasColumnName("last_accessed");
                client.Property(x => x.UserSsoLifetime).HasColumnName("user_sso_lifetime");
                client.Property(x => x.DeviceCodeLifetime).HasColumnName("device_code_lifetime");
                client.Property(x => x.NonEditable).HasColumnName("non_editable");

                client.HasIndex(x => x.ClientId).IsUnique();

                client.HasMany(x => x.AllowedGrantTypes).WithOne(x => x.Client).HasForeignKey(x=>x.ClientId).IsRequired().OnDelete(DeleteBehavior.Cascade);
                client.HasMany(x => x.RedirectUris).WithOne(x => x.Client).HasForeignKey(x => x.ClientId).IsRequired().OnDelete(DeleteBehavior.Cascade);
                client.HasMany(x => x.PostLogoutRedirectUris).WithOne(x => x.Client).HasForeignKey(x => x.ClientId).IsRequired().OnDelete(DeleteBehavior.Cascade);
                client.HasMany(x => x.AllowedScopes).WithOne(x => x.Client).HasForeignKey(x => x.ClientId).IsRequired().OnDelete(DeleteBehavior.Cascade);
                client.HasMany(x => x.ClientSecrets).WithOne(x => x.Client).HasForeignKey(x => x.ClientId).IsRequired().OnDelete(DeleteBehavior.Cascade);
                client.HasMany(x => x.Claims).WithOne(x => x.Client).HasForeignKey(x => x.ClientId).IsRequired().OnDelete(DeleteBehavior.Cascade);
                client.HasMany(x => x.IdentityProviderRestrictions).WithOne(x => x.Client).HasForeignKey(x => x.ClientId).IsRequired().OnDelete(DeleteBehavior.Cascade);
                client.HasMany(x => x.AllowedCorsOrigins).WithOne(x => x.Client).HasForeignKey(x => x.ClientId).IsRequired().OnDelete(DeleteBehavior.Cascade);
                client.HasMany(x => x.Properties).WithOne(x => x.Client).HasForeignKey(x => x.ClientId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ClientGrantType>(grantType =>
            {
                grantType.ToTable("client_grant_types");
                grantType.Property(x => x.Id).HasColumnName("id");
                grantType.Property(x => x.GrantType).HasMaxLength(250).IsRequired().HasColumnName("grant_type");
                grantType.Property(x => x.ClientId).HasColumnName("client_id");
            });

            modelBuilder.Entity<ClientRedirectUri>(redirectUri =>
            {
                redirectUri.ToTable("client_redirect_uris");
                redirectUri.Property(x => x.Id).HasColumnName("id");
                redirectUri.Property(x => x.RedirectUri).HasMaxLength(2000).IsRequired().HasColumnName("redirect_uri");
                redirectUri.Property(x => x.ClientId).HasColumnName("client_id");
            });

            modelBuilder.Entity<ClientPostLogoutRedirectUri>(postLogoutRedirectUri =>
            {
                postLogoutRedirectUri.ToTable("client_post_logout_redirect_uris");
                postLogoutRedirectUri.Property(x => x.Id).HasColumnName("id");
                postLogoutRedirectUri.Property(x => x.PostLogoutRedirectUri).HasMaxLength(2000).IsRequired().HasColumnName("post_logout_redirect_uri");
                postLogoutRedirectUri.Property(x => x.ClientId).HasColumnName("client_id");
            });

            modelBuilder.Entity<ClientScope>(scope =>
            {
                scope.ToTable("client_scopes");
                scope.Property(x => x.Id).HasColumnName("id");
                scope.Property(x => x.Scope).HasMaxLength(200).IsRequired().HasColumnName("scope");
                scope.Property(x => x.ClientId).HasColumnName("client_id");
            });

            modelBuilder.Entity<ClientSecret>(secret =>
            {
                secret.ToTable("client_secrets");
                secret.Property(x => x.Id).HasColumnName("id");
                secret.Property(x => x.Value).HasMaxLength(4000).IsRequired().HasColumnName("value");
                secret.Property(x => x.Type).HasMaxLength(250).IsRequired().HasColumnName("type");
                secret.Property(x => x.Description).HasMaxLength(2000).HasColumnName("description");
                secret.Property(x => x.ClientId).HasColumnName("client_id");
                secret.Property(x => x.Expiration).HasColumnName("expiration");
                secret.Property(x => x.Created).HasColumnName("created");
            });

            modelBuilder.Entity<ClientClaim>(claim =>
            {
                claim.ToTable("client_claims");
                claim.Property(x => x.Id).HasColumnName("id");
                claim.Property(x => x.Type).HasMaxLength(250).IsRequired().HasColumnName("type");
                claim.Property(x => x.Value).HasMaxLength(250).IsRequired().HasColumnName("value");
                claim.Property(x => x.ClientId).HasColumnName("client_id");
            });

            modelBuilder.Entity<ClientIdPRestriction>(idPRestriction =>
            {
                idPRestriction.ToTable("client_idp_restriction");
                idPRestriction.Property(x => x.Id).HasColumnName("id");
                idPRestriction.Property(x => x.Provider).HasMaxLength(200).IsRequired().HasColumnName("provider");
                idPRestriction.Property(x => x.ClientId).HasColumnName("client_id");
            });

            modelBuilder.Entity<ClientCorsOrigin>(corsOrigin =>
            {
                corsOrigin.ToTable("client_cors_origins");
                corsOrigin.Property(x => x.Id).HasColumnName("id");
                corsOrigin.Property(x => x.Origin).HasMaxLength(150).IsRequired().HasColumnName("origin");
                corsOrigin.Property(x => x.ClientId).HasColumnName("client_id");
            });

            modelBuilder.Entity<ClientProperty>(property =>
            {
                property.ToTable("client_properties");
                property.Property(x => x.Key).HasMaxLength(250).IsRequired().HasColumnName("key");
                property.Property(x => x.Value).HasMaxLength(2000).IsRequired().HasColumnName("value");
                property.Property(x => x.ClientId).HasColumnName("client_id");
            });
        }

        public static void CustomConfigureResourceContext(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityResource>(identityResource =>
            {
                identityResource.ToTable("identity_resources").HasKey(x => x.Id);

                identityResource.Property(x => x.Enabled).HasColumnName("enabled");
                identityResource.Property(x => x.Name).HasMaxLength(200).IsRequired().HasColumnName("name");
                identityResource.Property(x => x.DisplayName).HasMaxLength(200).HasColumnName("display_name");
                identityResource.Property(x => x.Description).HasMaxLength(1000).HasColumnName("description");
                identityResource.Property(x => x.Required).HasColumnName("required");
                identityResource.Property(x => x.Emphasize).HasColumnName("emphasize");
                identityResource.Property(x => x.ShowInDiscoveryDocument).HasColumnName("show_in_discovery_document");
                identityResource.Property(x => x.Created).HasColumnName("created");
                identityResource.Property(x => x.Updated).HasColumnName("updated");
                identityResource.Property(x => x.NonEditable).HasColumnName("non_editable");

                identityResource.HasIndex(x => x.Name).IsUnique();

                identityResource.HasMany(x => x.UserClaims).WithOne(x => x.IdentityResource).HasForeignKey(x => x.IdentityResourceId).IsRequired().OnDelete(DeleteBehavior.Cascade);
                identityResource.HasMany(x => x.Properties).WithOne(x => x.IdentityResource).HasForeignKey(x => x.IdentityResourceId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<IdentityClaim>(claim =>
            {
                claim.ToTable("identity_claims").HasKey(x => x.Id);

                claim.Property(x => x.Id).HasColumnName("id");
                claim.Property(x => x.Type).HasMaxLength(200).IsRequired().HasColumnName("type");
                claim.Property(x => x.IdentityResourceId).HasColumnName("identity_resource_id");
            });

            modelBuilder.Entity<IdentityResourceProperty>(property =>
            {
                property.ToTable("identity_resource_properties");
                property.Property(x => x.Id).HasColumnName("id");
                property.Property(x => x.Key).HasMaxLength(250).IsRequired().HasColumnName("key");
                property.Property(x => x.Value).HasMaxLength(2000).IsRequired().HasColumnName("value");
                property.Property(x => x.IdentityResourceId).HasColumnName("identity_resource_id");
            });



            modelBuilder.Entity<ApiResource>(apiResource =>
            {
                apiResource.ToTable("api_resources").HasKey(x => x.Id);

                apiResource.Property(x => x.Id).HasColumnName("id");
                apiResource.Property(x => x.Name).HasMaxLength(200).IsRequired().HasColumnName("name");
                apiResource.Property(x => x.DisplayName).HasMaxLength(200).HasColumnName("display_name");
                apiResource.Property(x => x.Description).HasMaxLength(1000).HasColumnName("description");
                apiResource.Property(x => x.Enabled).HasColumnName("enabled");
                apiResource.Property(x => x.Created).HasColumnName("created");
                apiResource.Property(x => x.Updated).HasColumnName("updated");
                apiResource.Property(x => x.LastAccessed).HasColumnName("last_accessed");
                apiResource.Property(x => x.NonEditable).HasColumnName("non_editable");

                apiResource.HasIndex(x => x.Name).IsUnique();

                apiResource.HasMany(x => x.Secrets).WithOne(x => x.ApiResource).HasForeignKey(x => x.ApiResourceId).IsRequired().OnDelete(DeleteBehavior.Cascade);
                apiResource.HasMany(x => x.Scopes).WithOne(x => x.ApiResource).HasForeignKey(x => x.ApiResourceId).IsRequired().OnDelete(DeleteBehavior.Cascade);
                apiResource.HasMany(x => x.UserClaims).WithOne(x => x.ApiResource).HasForeignKey(x => x.ApiResourceId).IsRequired().OnDelete(DeleteBehavior.Cascade);
                apiResource.HasMany(x => x.Properties).WithOne(x => x.ApiResource).HasForeignKey(x => x.ApiResourceId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ApiSecret>(apiSecret =>
            {
                apiSecret.ToTable("api_secrets").HasKey(x => x.Id);

                apiSecret.Property(x => x.Id).HasColumnName("id");
                apiSecret.Property(x => x.Description).HasMaxLength(1000).HasColumnName("description");
                apiSecret.Property(x => x.Value).HasMaxLength(4000).IsRequired().HasColumnName("value");
                apiSecret.Property(x => x.Type).HasMaxLength(250).IsRequired().HasColumnName("type");
                apiSecret.Property(x => x.Expiration).HasColumnName("expiration");
                apiSecret.Property(x => x.ApiResourceId).HasColumnName("api_resource_id");
                apiSecret.Property(x => x.Created).HasColumnName("created");
            });

            modelBuilder.Entity<ApiResourceClaim>(apiClaim =>
            {
                apiClaim.ToTable("api_claims").HasKey(x => x.Id);

                apiClaim.Property(x => x.Id).HasColumnName("id");
                apiClaim.Property(x => x.Type).HasMaxLength(200).IsRequired().HasColumnName("type");
                apiClaim.Property(x => x.ApiResourceId).HasColumnName("api_resource_id");
            });

            modelBuilder.Entity<ApiScope>(apiScope =>
            {
                apiScope.ToTable("api_scopes").HasKey(x => x.Id);

                apiScope.Property(x => x.Id).HasColumnName("id");
                apiScope.Property(x => x.Name).HasMaxLength(200).IsRequired().HasColumnName("name");
                apiScope.Property(x => x.DisplayName).HasMaxLength(200).HasColumnName("display_name");
                apiScope.Property(x => x.Description).HasMaxLength(1000).HasColumnName("description");
                apiScope.Property(x => x.Required).HasColumnName("required");
                apiScope.Property(x => x.Emphasize).HasColumnName("emphasize");
                apiScope.Property(x => x.ShowInDiscoveryDocument).HasColumnName("show_in_discovery_document");
                apiScope.Property(x => x.ApiResourceId).HasColumnName("api_resource_id");

                apiScope.HasIndex(x => x.Name).IsUnique();

                apiScope.HasMany(x => x.UserClaims).WithOne(x => x.ApiScope).HasForeignKey(x => x.ApiScopeId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ApiScopeClaim>(apiScopeClaim =>
            {
                apiScopeClaim.ToTable("api_scope_claims").HasKey(x => x.Id);

                apiScopeClaim.Property(x => x.Id).HasColumnName("id");
                apiScopeClaim.Property(x => x.Type).HasMaxLength(200).IsRequired().HasColumnName("type");
                apiScopeClaim.Property(x => x.ApiScopeId).HasColumnName("api_scope_id");
            });

            modelBuilder.Entity<ApiResourceProperty>(property =>
            {
                property.ToTable("api_resource_properties");
                property.Property(x => x.Key).HasMaxLength(250).IsRequired().HasColumnName("key");
                property.Property(x => x.Value).HasMaxLength(2000).IsRequired().HasColumnName("value");
                property.Property(x => x.ApiResourceId).HasColumnName("api_resource_id");
            });
        }

        public static void CustomConfigurePersistedGrantContext(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersistedGrant>(grant =>
            {
                grant.ToTable("persisted_grants");

                grant.Property(x => x.Key).HasMaxLength(200).ValueGeneratedNever().HasColumnName("key");
                grant.Property(x => x.Type).HasMaxLength(50).IsRequired().HasColumnName("value");
                grant.Property(x => x.SubjectId).HasMaxLength(200).HasColumnName("subject_id");
                grant.Property(x => x.ClientId).HasMaxLength(200).IsRequired().HasColumnName("client_id");
                grant.Property(x => x.CreationTime).IsRequired().HasColumnName("creation_time");
                grant.Property(x => x.Expiration).IsRequired().HasColumnName("expiration");
                // 50000 chosen to be explicit to allow enough size to avoid truncation, yet stay beneath the MySql row size limit of ~65K
                // apparently anything over 4K converts to nvarchar(max) on SqlServer
                grant.Property(x => x.Data).HasMaxLength(50000).IsRequired().HasColumnName("data");

                grant.HasKey(x => x.Key);

                grant.HasIndex(x => new { x.SubjectId, x.ClientId, x.Type });
                grant.HasIndex(x => x.Expiration);
            });

            modelBuilder.Entity<DeviceFlowCodes>(codes =>
            {
                codes.ToTable("device_flows_codes");

                codes.Property(x => x.DeviceCode).HasMaxLength(200).IsRequired().HasColumnName("device_code");
                codes.Property(x => x.UserCode).HasMaxLength(200).IsRequired().HasColumnName("user_code");
                codes.Property(x => x.SubjectId).HasMaxLength(200).HasColumnName("subject_id");
                codes.Property(x => x.ClientId).HasMaxLength(200).IsRequired().HasColumnName("client_id");
                codes.Property(x => x.CreationTime).IsRequired().HasColumnName("creation_time");
                codes.Property(x => x.Expiration).IsRequired().HasColumnName("expiration");
                // 50000 chosen to be explicit to allow enough size to avoid truncation, yet stay beneath the MySql row size limit of ~65K
                // apparently anything over 4K converts to nvarchar(max) on SqlServer
                codes.Property(x => x.Data).HasMaxLength(50000).IsRequired().HasColumnName("data");

                codes.HasKey(x => new {x.UserCode});

                codes.HasIndex(x => x.DeviceCode).IsUnique();
                codes.HasIndex(x => x.Expiration);
            });
        }
    }
}