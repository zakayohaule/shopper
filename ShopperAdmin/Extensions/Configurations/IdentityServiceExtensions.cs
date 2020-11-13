using System;
using System.Text;
using ShopperAdmin.Database;
using Shared.Mvc.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Shared.Mvc.Entities.Identity;

namespace ShopperAdmin.Extensions.Configurations
{
    public static class IdentityServiceExtensions
    {
        public static void ConfigureIdentity(this IServiceCollection services, IWebHostEnvironment environment,
            IConfiguration configuration)
        {
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
                    }

                    ;

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

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddJwtBearer("jwt", options =>
                {
                    options.Authority = "http://localhost:5000";
                    options.SaveToken = true;

                    // todo Remove this in production
                    options.RequireHttpsMetadata = false;
                    options.Audience = "egov_apis";
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // IssuerSigningKey = new SymmetricSecurityKey()
                        ClockSkew = TimeSpan.Zero,
                        ValidateIssuer = true,
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
                options.ReturnUrlParameter = "return_to";
            });
        }
    }
}