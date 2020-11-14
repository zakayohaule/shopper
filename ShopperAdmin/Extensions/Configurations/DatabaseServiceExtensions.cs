using ShopperAdmin.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
        }
    }
}