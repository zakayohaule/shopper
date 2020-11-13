using System;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Shopper.Extensions.Configurations
{
    public static class MvcServiceExtensions
    {
        public static void ConfigureMvc(this IServiceCollection services)
        {
            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                })
                .AddFluentValidation(configuration =>
                {
                    configuration.RegisterValidatorsFromAssemblyContaining<Startup>();
                });

            services.AddRouting(options => { options.LowercaseUrls = true; });

            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationFormats.Clear();
                options.ViewLocationFormats.Add("/Mvc/Views/{1}/{0}" + RazorViewEngine.ViewExtension);
                options.ViewLocationFormats.Add("/Mvc/Views/Shared/{0}" + RazorViewEngine.ViewExtension);
                options.ViewLocationFormats.Add("/Mvc/Views/Shared/Partials/{0}" + RazorViewEngine.ViewExtension);
            });

            services.AddMemoryCache(options => options.ExpirationScanFrequency = TimeSpan.FromMinutes(30));

        }
    }
}