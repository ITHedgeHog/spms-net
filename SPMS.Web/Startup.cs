using System;
using System.Reflection;
using System.Threading.Tasks;
using Amazon.Runtime;
using Amazon.S3;
using AspNetCore.DataProtection.Aws.S3;
using AutoMapper;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.FeatureManagement;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using SPMS.Web.Filter;
using SPMS.Web.Models;
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



            Environment.SetEnvironmentVariable("AWS_ACCESS_KEY_ID", Configuration["AWS:AccessKey"]);
            Environment.SetEnvironmentVariable("AWS_SECRET_ACCESS_KEY", Configuration["AWS:SecretKey"]);



            services.AddDbContext<SpmsContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("SpmsContextSql")));

            // using Microsoft.AspNetCore.DataProtection;
            //services.AddDataProtection()
            //    .PersistKeysToDbContext<SpmsContext>();

            ///https://static-btd.ams3.digitaloceanspaces.com
            ///


            var s3config = new AmazonS3Config()
            {

                ServiceURL = "https://ams3.digitaloceanspaces.com"
            };
            // Configure your AWS SDK however you usually would do so e.g. IAM roles, environment variables
            services.TryAddSingleton<IAmazonS3>(new AmazonS3Client(s3config)
            {

            });

            // Assumes a Configuration property set as IConfigurationRoot similar to ASP.NET docs
            services.AddDataProtection()
                .SetApplicationName(Configuration.GetValue<string>("cluster-name")) // Not required by S3 storage but a requirement for server farms
                .PersistKeysToAwsS3(Configuration.GetSection("S3Persistence"));



            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // Add authentication services
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie()
            .AddOpenIdConnect("Auth0", options =>
            {
                // Set the authority to your Auth0 domain
                options.Authority = $"https://{Configuration["Auth0:Domain"]}";

                // Configure the Auth0 Client ID and Client Secret
                options.ClientId = Configuration["Auth0:ClientId"];
                options.ClientSecret = Configuration["Auth0:ClientSecret"];

                // Set response type to code
                options.ResponseType = OpenIdConnectResponseType.Code;

                // Configure the scope
                options.Scope.Add("openid");

                // Set the callback path, so Auth0 will call back to http://localhost:3000/callback
                // Also ensure that you have added the URL as an Allowed Callback URL in your Auth0 dashboard
                options.CallbackPath = new PathString("/callback");

                // Configure the Claims Issuer to be Auth0
                options.ClaimsIssuer = "Auth0";

                // Configure the scope
                options.Scope.Clear();
                options.Scope.Add("openid");
                options.Scope.Add("profile");
                options.Scope.Add("email");
                //     options.Scope.Add("roles");

                // Set the correct name claim type
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = "name",
                    RoleClaimType = "https://schemas.quickstarts.com/roles"
                };

                options.Events = new OpenIdConnectEvents
                {
                    // handle the logout redirection
                    OnRedirectToIdentityProviderForSignOut = (context) =>
                    {
                        var logoutUri = $"https://{Configuration["Auth0:Domain"]}/v2/logout?client_id={Configuration["Auth0:ClientId"]}";

                        var postLogoutUri = context.Properties.RedirectUri;
                        if (!string.IsNullOrEmpty(postLogoutUri))
                        {
                            if (postLogoutUri.StartsWith("/"))
                            {
                                // transform to absolute
                                var request = context.Request;
                                postLogoutUri = request.Scheme + "://" + request.Host + request.PathBase + postLogoutUri;
                            }
                            logoutUri += $"&returnTo={ Uri.EscapeDataString(postLogoutUri)}";
                        }

                        context.Response.Redirect(logoutUri);
                        context.HandleResponse();

                        return Task.CompletedTask;
                    },
                    OnRemoteFailure = (ctx) =>
                    {
                        TelemetryConfiguration tc1 = TelemetryConfiguration.CreateDefault();
                        tc1.InstrumentationKey = Configuration.GetValue<string>("APPINSIGHTS_INSTRUMENTATIONKEY");

                        var client = new TelemetryClient(tc1);
                        client.TrackException(ctx.Failure);
                        //ai.
                        // React to the error here. See the notes below.
                        return Task.CompletedTask;
                    }
                };
            });


            // Add AutoMapper
            services.AddAutoMapper(typeof(Startup));



            // Add Services
            services.AddHttpContextAccessor();
            services.AddTransient<IGameService, GameService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IStoryService, StoryService>();
            ///services.AddTransient<IMarkdownService, MarkdownService>();
            services.AddTransient<IAuthoringService, AuthoringService>();
           



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

            services.AddControllersWithViews(opt => opt.Filters.Add(typeof(ViewModelFilter))).AddRazorRuntimeCompilation().AddApplicationPart(typeof(MarkdownPageProcessorMiddleware).Assembly);
            services.AddApplicationInsightsTelemetry(Configuration["APPINSIGHTS_INSTRUMENTATIONKEY"]);
            services.AddFeatureManagement();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, SpmsContext context, IMapper mapper)
        {
            mapper.ConfigurationProvider.AssertConfigurationIsValid();

            app.Use((httpContext, next) =>
            {
                if (httpContext.Request.Headers["x-forwarded-proto"] == "https")
                {
                    httpContext.Request.Scheme = "https";
                }
                return next();
            });


            //context.Database.EnsureDeleted();
            if (Configuration.GetValue<bool>("MigrateAndSeed"))
            {
                context.Database.Migrate();
                Seed.SeedDefaults(context);
            }

            if (Configuration.GetValue<bool>("SeedBtd"))
            {
                Seed.SeedBtd(context);
            }
            if (env.IsDevelopment() || Configuration.GetValue<bool>("ShowErrors"))
            {
                app.UseDeveloperExceptionPage();

            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseMiniProfiler();
            app.UseHttpsRedirection();
            app.UseMarkdown();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllerRoute(
                    name: "admin",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            app.UseAzureAppConfiguration();
        }
    }
}
