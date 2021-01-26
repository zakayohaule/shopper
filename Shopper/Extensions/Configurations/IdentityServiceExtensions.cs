﻿using System;
 using Microsoft.AspNetCore.Authentication.Cookies;
 using Microsoft.AspNetCore.Hosting;
 using Microsoft.AspNetCore.Http;
 using Microsoft.AspNetCore.Identity;
 using Microsoft.Extensions.Configuration;
 using Microsoft.Extensions.DependencyInjection;
 using Microsoft.Extensions.Hosting;
 using Microsoft.IdentityModel.Tokens;
 using Shopper.Database;
 using Shopper.Extensions.Helpers;
 using Shopper.Mvc.Entities.Identity;
 using Shopper.Other;

 namespace Shopper.Extensions.Configurations
{
    public static class IdentityServiceExtensions
    {
        public static void ConfigureIdentity(this IServiceCollection services, IWebHostEnvironment environment,
            IConfiguration configuration)
        {
            var serviceProvider = services.BuildServiceProvider();

            services.AddIdentity<AppUser, Role>(options =>
                {
                    if (environment.IsDevelopment())
                    {
                        //Password settings
                        options.Password.RequiredLength = 6;
                        options.Password.RequireDigit = false;
                        options.Password.RequireLowercase = false;
                        options.Password.RequireUppercase = false;
                        options.Password.RequireNonAlphanumeric = false;
                    };

                    //User settings
                    options.User.RequireUniqueEmail = true;

                    //Lockout settings
                    options.Lockout.AllowedForNewUsers = true;
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
                    options.Lockout.MaxFailedAccessAttempts = 5;

                    if (environment.IsDevelopment())
                    {
                        options.SignIn.RequireConfirmedAccount = true;
                        options.SignIn.RequireConfirmedEmail = true;
                    }
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddIdentityServer()
                .AddInMemoryApiResources(IdentityConfig.Apis)
                .AddInMemoryClients(IdentityConfig.Clients)
                .AddDeveloperSigningCredential();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddJwtBearer("jwt", options =>
                {
                    var httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
                    var tenant = httpContextAccessor.HttpContext.GetCurrentTenant();
                    var tenantUrl = configuration.GetValue<string>("TokenAuthority").Replace("{sub}.", "");

                    options.Authority = tenantUrl;
                    options.SaveToken = true;

                    // todo Remove this in production
                    options.RequireHttpsMetadata = false;
                    options.Audience = "add_user";
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // IssuerSigningKey = new SymmetricSecurityKey()
                        ClockSkew = TimeSpan.Zero,
                        ValidIssuer = tenantUrl,
                        ValidateIssuer = false,
                        ValidateLifetime = true,
                        RequireExpirationTime = true,
                        ValidateAudience = true,
                        RequireAudience = true,
                    };
                });

            services.ConfigureApplicationCookie(options =>
            {
                var cookieExpiration = configuration.GetSection("Cookie")?.GetValue<int?>("Expires-Minutes") ?? 60;

                // options.Cookie.Name = "api-auth-server";
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(cookieExpiration);
                options.LoginPath = "/account/login";
                options.LogoutPath = "/account/logout";
                options.ReturnUrlParameter = "return-to";
            });
        }
    }
}
