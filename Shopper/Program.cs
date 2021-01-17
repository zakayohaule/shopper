using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Filters;
using Shopper.Extensions.Configurations;

namespace Shopper
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.ColoredConsole(LogEventLevel.Information)
                .CreateLogger();

            var host = CreateHostBuilder(args).Build();
            try
            {
                Log.Logger.Information("************* Starting Application *************");
                host.SeedDatabase().Run();
            }
            catch (Exception e)
            {
                Log.Logger.Fatal($"************* Application Failed To Start: {e.Message} *************");
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                Log.CloseAndFlush();
            }

            // CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                        .UseKestrel(options => options.Limits.MaxRequestBodySize = null);
                })
                .ConfigureLogging(builder => builder.ClearProviders())
                .UseSerilog((context, configuration) =>
                {
                    if (context.HostingEnvironment.IsDevelopment())
                    {
                        configuration.WriteTo.Logger(lg =>
                        {
                            lg.Filter.ByExcluding(Matching.FromSource("Microsoft.EntityFrameworkCore.Database.Command"))
                                .WriteTo
                                .ColoredConsole(restrictedToMinimumLevel: LogEventLevel.Information);
                        });
                    }

                    //Write information logs
                    /*if (context.HostingEnvironment.IsDevelopment())
                    {
                        configuration.WriteTo.Logger(lg =>
                        {
                            lg.Filter.ByIncludingOnly(logEvent
                                    => logEvent.Level == LogEventLevel.Information ||
                                       logEvent.Level == LogEventLevel.Warning
                                )
                                .Filter.ByExcluding(Matching.FromSource("Serilog.AspNetCore.RequestLoggingMiddleware"))
                                .WriteTo.File(@"Logs\Info\info_.log",
                                    rollingInterval: RollingInterval.Day,
                                    outputTemplate:
                                    "[{Timestamp:HH:mm:ss} {Level:u3}] [{SourceContext}] {NewLine} {Message:lj}{NewLine}{Exception}");
                        });
                    }*/


                    //Write error logs
                    /*configuration.WriteTo.Logger(lg =>
                    {
                        lg.Filter.ByIncludingOnly(logEvent
                            => logEvent.Level == LogEventLevel.Error ||
                               logEvent.Level == LogEventLevel.Fatal
                        ).WriteTo.File(path: @"Logs\Errors\error_.log",
                            rollingInterval: RollingInterval.Day,
                            outputTemplate:
                            "{Timestamp:HH:mm:ss} [{Level:u3}] [{SourceContext}] {NewLine} {Message:lj}{NewLine}{Exception}");
                    });*/
                    /*configuration.WriteTo.Logger(lg =>
                    {
                        lg.MinimumLevel.Debug()
                            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                            // Filter out ASP.NET Core infrastructre logs that are Information and below
                            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                        // lg
                            .Filter.ByIncludingOnly(Matching.FromSource("Serilog.AspNetCore.RequestLoggingMiddleware"))
                            /*.Filter.ByExcluding(Matching.FromSource("Microsoft"))
                            .Filter.ByExcluding(Matching.FromSource("Microsoft.AspNetCore"))#1#
                            .WriteTo.File(path: @"Logs\Requests\request_.log",
                                rollingInterval: RollingInterval.Day,
                                outputTemplate:
                                "{Timestamp:HH:mm:ss} [{Level:u3}] [{SourceContext}] {NewLine} {Message:lj}{NewLine}{Exception}");
                    });*/
                });
    }
}
