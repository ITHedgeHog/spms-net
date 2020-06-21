using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SPMS.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
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
