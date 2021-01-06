using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Shopper.Extensions.Configurations
{
    public static class EmailServiceExtensions
    {
        public static void ConfigureMailServer(this IServiceCollection services, IWebHostEnvironment environment)
        {
            var builder = services.AddFluentEmail("zackhaule@gmail.com")
                .AddRazorRenderer();

            if (environment.IsDevelopment())
            {
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    EnableSsl = true,
                    Credentials = new NetworkCredential("zackhaule@gmail.com", "@BoH0810"),
                };
                // builder.AddSmtpSender("zackhaule@Gmail.com", "@BoH0810", "smtp.gmail.com", 587);
                builder.AddSmtpSender(smtpClient);
                // builder.AddMailtrapSender("51f7f8fd58db29", "3ef16ce8493234", "smtp.mailtrap.io", 2525);
            }
            else
            {
                builder.AddSmtpSender("some-production-mail-server", 000, "username", "password");
            }
        }
    }
}
