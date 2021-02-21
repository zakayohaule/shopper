using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Shopper.Extensions.Configurations
{
    public static class MvcServiceExtensions
    {
        public static void ConfigureMvc(this IServiceCollection services)
        {
            // @todo create middleware to log request time

            // services.AddLocalization(options => { options.ResourcesPath = "Resources"; });


            services.AddControllersWithViews()
                // .AddViewLocalization(options => options.ResourcesPath = "Resources")
                // .AddDataAnnotationsLocalization()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });


            /*services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new List<CultureInfo>
                {
                    new CultureInfo("en"),
                    new CultureInfo("sw")
                };
                options.RequestCultureProviders = new List<IRequestCultureProvider>
                {
                    new QueryStringRequestCultureProvider(),
                    new CookieRequestCultureProvider()
                };
                options.FallBackToParentCultures = false;
                options.FallBackToParentUICultures = false;
                options.DefaultRequestCulture = new RequestCulture("en", "en");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });*/

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
