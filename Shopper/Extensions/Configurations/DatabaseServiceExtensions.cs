using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shopper.Database;

namespace Shopper.Extensions.Configurations
{
    public static class DatabaseServiceExtensions
    {
        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>();
            services.AddDbContext<AdminAppDbContext>(builder =>
            {
                builder.UseMySql(configuration.GetConnectionString("AdminApp"));
            });
        }
    }
}
