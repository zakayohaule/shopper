using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ShopperAdmin.Extensions.Configurations
{
    public static class EmailServiceExtensions
    {
        public static void ConfigureMailServer(this IServiceCollection services, IWebHostEnvironment environment)
        {
            var builder = services.AddFluentEmail("admin@ega.go.tz")
                .AddRazorRenderer();

            if (environment.IsDevelopment())
            {
                builder.AddMailtrapSender("e8c9a211be6576", "f7072de5b40e9d", "smtp.mailtrap.io", 2525);
            }
            else
            {
                builder.AddSmtpSender("some-production-mail-server", 000, "username", "password");
            }
        }
    }
}