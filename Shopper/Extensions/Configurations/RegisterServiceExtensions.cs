using Shared.Mvc.Entities;
using Shopper.Services.Implementations;
using Shopper.Services.Implementations.ReplaceDefaults;
using Shopper.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Shared.Mvc.Entities.Identity;

namespace Shopper.Extensions.Configurations
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
            services.AddScoped<ITenantService, TenantService>();
            services.AddScoped<IPriceTypeService, PriceTypeService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductGroupService, ProductGroupService>();
            services.AddScoped<IProductCategoryService, ProductCategoryService>();
            services.AddScoped<IAttributeService, AttributeService>();
            services.AddScoped<IAttributeOptionService, AttributeOptionService>();
        }
    }
}
