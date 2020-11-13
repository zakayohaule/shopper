using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Shared.Mvc.Entities.Identity;
using ShopperAdmin.Services.Implementations;
using ShopperAdmin.Services.Implementations.ReplaceDefaults;
using ShopperAdmin.Services.Interfaces;

namespace ShopperAdmin.Extensions.Configurations
{
    public static class RegisterServiceExtensions
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddHostedService<EmailSender>();
            services.AddSingleton<IEmailQueueService, EmailQueueService>();
        }

        public static void ReplaceDefaultServices(this IServiceCollection services)
        {
            services.AddScoped<IUserClaimsPrincipalFactory<AppUser>, AppUserClaimsPrincipalFactory>();
            services.AddScoped<IUserClaimService, UserClaimService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<INotificationService, NotificationService>();
        }
    }
}