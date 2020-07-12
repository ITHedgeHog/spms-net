using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.FeatureManagement;
using Microsoft.Identity.Web;
using SPMS.Application.Common.Interfaces;
using SPMS.Web.Areas.player.Hubs;
using SPMS.Web.Filter;
using SPMS.Web.Policy;
using SPMS.Web.Service;
using Westwind.AspNetCore.Markdown;

namespace SPMS.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            SPMS.Infrastructure.DependencyInjection.AddInfrastructure(services, Configuration);
            SPMS.Persistence.MSSQL.DependencyInjection.AddPersistence(services, Configuration);
            SPMS.Application.DependencyInjection.AddApplication(services);
            services.AddTransient<ICurrentUserService, CurrentUserService>();
            services.AddHttpContextAccessor();

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.Unspecified;
                // Handling SameSite cookie according to https://docs.microsoft.com/en-us/aspnet/core/security/samesite?view=aspnetcore-3.1
                options.HandleSameSiteCookieCompatibility();
            });

            services.AddSignIn(Configuration, "AzureAdB2C");

            // Policies
            services.AddAuthorization(options =>
            {
                options.AddPolicy(
                    "Administrator", policy =>
                        policy.Requirements.Add(
                            new AdministratorRequirement()));
                options.AddPolicy("Player", policy => policy.Requirements.Add(new PlayerRequirement()));
            });


            services.AddTransient<IAuthorizationHandler, AdministratorHandler>();
            services.AddTransient<IAuthorizationHandler, PlayerPolicyHandler>();


            // Profiler
            services.AddMiniProfiler(options =>
            {
                // All of this is optional. You can simply call .AddMiniProfiler() for all defaults

                // (Optional) Path to use for profiler URLs, default is /mini-profiler-resources
                options.RouteBasePath = "/profiler";


            }).AddEntityFramework();

            services.AddMarkdown();



            services.AddRazorPages();
            services.AddControllersWithViews(opt => opt.Filters.Add(typeof(ViewModelFilter)))
                .AddRazorRuntimeCompilation().AddApplicationPart(typeof(MarkdownPageProcessorMiddleware).Assembly);

            services.AddApplicationInsightsTelemetry(Configuration["APPINSIGHTS_INSTRUMENTATIONKEY"]);
            services.AddFeatureManagement();

            services.AddSignalR(o => o.EnableDetailedErrors = true)
               .AddMessagePackProtocol().AddAzureSignalR();

            var cfg = Configuration.GetSection("AzureAdB2C");
            services.Configure<OpenIdConnectOptions>(cfg);
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //TODO: Enable if we're on Kubernetes, with SSL terminated else where.
            //app.Use(async (httpContext, next) =>
            //{
            //    if (httpContext.Request.Headers["x-forwarded-proto"] == "https")
            //    {
            //        httpContext.Request.Scheme = "https";
            //    }
            //    await next();
            //});


            if (env.IsDevelopment() || Configuration.GetValue<bool>("ShowErrors"))
            {
                app.UseDeveloperExceptionPage();

            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }



            app.UseAzureAppConfiguration();
            app.UseMiniProfiler();
            app.UseHttpsRedirection();
            app.UseMarkdown();
            app.UseStaticFiles();

            


            app.UseCookiePolicy();
            app.UseAuthentication();
            
            
            
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();

                endpoints.MapHub<AuthoringHub>("/authoringHub");
                endpoints.MapAreaControllerRoute(name: "MicrosoftIdentity",
                    pattern: "MicrosoftIdentity/{controller}/{action}/{id?}",
                    areaName: "MicrosoftIdentity"
                );

                endpoints.MapAreaControllerRoute(name: "admin",
                    pattern: "admin/{controller=Home}/{action=Index}/{id?}",
                    areaName: "admin"
                );
                endpoints.MapAreaControllerRoute(name: "player",
                    pattern: "player/{controller=Home}/{action=Index}/{id?}",
                    areaName: "player"
                );
                endpoints.MapDefaultControllerRoute();
            });
            
        }
    }
}
