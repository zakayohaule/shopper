using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShopperAdmin.Database;

namespace ShopperAdmin.Extensions.Configurations
{
    public static class DatabaseServiceExtensions
    {
        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(builder =>
            {
                builder.UseMySql(configuration.GetConnectionString("Default"));
            });
            services.AddDbContext<TenantDbContext>();
            /*services.AddDbContext<AdminAppDbContext>(builder =>
            {
                builder.UseMySql(configuration.GetConnectionString("AdminApp"));
            });*/
        }
    }
}
