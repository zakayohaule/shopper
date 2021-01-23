using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using ShopperAdmin.Extensions.Configurations;

[assembly: AspMvcViewLocationFormat(@"~\Mvc\Views\{1}\{0}.cshtml")]
[assembly: AspMvcViewLocationFormat(@"~\Mvc\Views\Shared\{0}.cshtml")]
[assembly: AspMvcPartialViewLocationFormat(@"~\Mvc\Views\Shared\{0}.cshtml")]
[assembly: AspMvcPartialViewLocationFormat(@"~\Mvc\Views\Shared\Partials\{0}.cshtml")]

namespace ShopperAdmin
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        private IConfiguration Configuration { get; }
        private IWebHostEnvironment Environment { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var showBanner = Configuration.GetValue<bool?>("ShowBanner") ?? false;
            if (showBanner)
            {
                AppBuilderExtensions.ShowBanner();
            }

            services.ConfigureMvc();

            services.ConfigureDatabase(Configuration);

            services.ConfigureIdentity(Environment, Configuration);

            services.ConfigureMailServer(Environment);

            services.ReplaceDefaultServices();

            services.RegisterServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                // app.UseHsts();
            }

            // app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSerilogRequestLogging();
            app.UseEndpoints(endpoints => { endpoints.MapDefaultControllerRoute(); });
        }
    }
}
