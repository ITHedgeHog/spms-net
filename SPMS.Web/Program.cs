using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SPMS.Application.Common.Interfaces;
using SPMS.Application.Services;
using SPMS.Application.System.Commands;
using SPMS.Common;
using SPMS.Persistence.MSSQL;

namespace SPMS.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            try
            {

                var host = CreateHostBuilder(args).Build();

                using (var scope = host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;

                    try
                    {
                        var spmsContext = services.GetService<SpmsContext>();

                        await spmsContext.Database.MigrateAsync();

                        var mediator = services.GetRequiredService<IMediator>();

                        await mediator.Send(new BasicDataSeederCommand());

                    }
                    catch (Exception ex)
                    {
                        var logger = services.GetRequiredService<ILogger<Program>>();
                        logger.LogError(ex, "An error occurred whilst migrating or initialising the database.");
                    }
                } 

                host.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine(ex.InnerException.Message);
                }
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                            .ConfigureWebHostDefaults(webBuilder =>
                            {
                                webBuilder.ConfigureAppConfiguration((hostingContext, config) =>
                                {
                                    var settings = config.Build();
                                    config.AddAzureAppConfiguration(o => o.Connect(Environment.GetEnvironmentVariable("APPCONFIG"))
                                        // Load configuration values with no label
                                        .Select(KeyFilter.Any, LabelFilter.Null)
                                        // Override with any configuration values specific to current hosting env
                                        .Select(KeyFilter.Any, hostingContext.HostingEnvironment.EnvironmentName)
                                        .ConfigureRefresh(o =>
                                        {

                                            o.Register("Sentinel", refreshAll: true)
                                                                       .SetCacheExpiration(TimeSpan.FromMinutes(int.Parse(Environment.GetEnvironmentVariable("CACHETIMEOUT"))));
                                        })
                                        .UseFeatureFlags(o =>
                                        {
                                            o.CacheExpirationTime = TimeSpan.FromMinutes(int.Parse(Environment.GetEnvironmentVariable("CACHETIMEOUT")));
                                        }));
                                }).UseStartup<Startup>();
                            });
        }
    }
}
