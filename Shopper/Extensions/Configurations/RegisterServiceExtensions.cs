using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Shared.Mvc.Entities.Identity;
using Shopper.Services;
using Shopper.Services.Implementations;
using Shopper.Services.Implementations.ReplaceDefaults;
using Shopper.Services.Interfaces;

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
            services.AddScoped<ITenantIdentifierService, TenantIdentifierService>();
            services.AddScoped<ITenantService, TenantService>();
            services.AddScoped<IPriceTypeService, PriceTypeService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductGroupService, ProductGroupService>();
            services.AddScoped<IProductCategoryService, ProductCategoryService>();
            services.AddScoped<IProductTypeService, ProductTypeService>();
            services.AddScoped<IAttributeService, AttributeService>();
            services.AddScoped<IAttributeOptionService, AttributeOptionService>();
            services.AddScoped<ISaleService, SaleService>();
            services.AddScoped<IFileUploadService, FileUploadService>();
            services.AddScoped<IExpenditureTypeService, ExpenditureTypeService>();
            services.AddScoped<IExpenditureService, ExpenditureService>();
            services.AddScoped<TenantResolver>();
            services.AddScoped<IBusinessService, BusinessService>();
            services.AddScoped<IReportService, ReportService>();
        }
    }
}
