using ShopperAdmin.Database;
using ShopperAdmin.Extensions.Configurations;
using Shared.Mvc.Entities;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Shared.Mvc.Entities.Identity;

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

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; set; }

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
        public void Configure(
            IApplicationBuilder app,
            [FromServices] UserManager<AppUser> userManager,
            [FromServices] ApplicationDbContext dbContext,
            [FromServices] ILogger logger)
        {
            //If you want to initialize the database, go to appSettings.json and set "Database.Seed" to true;
            var seedDatabase = Configuration.GetSection("Database").GetValue<bool?>("Seed") ?? false;
            if (seedDatabase)
            {
                logger.Information("********** Seeding database *************");
                app.InitializeDatabase(dbContext, userManager, logger);
            }

            app.UpdateRoleClaims(dbContext, logger);

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