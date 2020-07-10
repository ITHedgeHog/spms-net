using System;
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
using Microsoft.Identity.Web.UI;
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

            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.Unspecified;
            //    options.OnAppendCookie = cookieContext =>
            //        CheckSameSite(cookieContext.Context, cookieContext.CookieOptions);
            //    options.OnDeleteCookie = cookieContext =>
            //        CheckSameSite(cookieContext.Context, cookieContext.CookieOptions);
            //});

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.Unspecified;
                // Handling SameSite cookie according to https://docs.microsoft.com/en-us/aspnet/core/security/samesite?view=aspnetcore-3.1
                options.HandleSameSiteCookieCompatibility();
            });

            services.AddSignIn(Configuration, "AzureAdB2C");
            //// Add authentication services
            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //})
            //.AddCookie()
            //.AddOpenIdConnect("Auth0", options =>
            //{
            //    // Set the authority to your Auth0 domain
            //    options.Authority = $"https://{Configuration["Auth0:Domain"]}";

            //    // Configure the Auth0 Client ID and Client Secret
            //    options.ClientId = Configuration["Auth0:ClientId"];
            //    options.ClientSecret = Configuration["Auth0:ClientSecret"];

            //    // Set response type to code
            //    options.ResponseType = OpenIdConnectResponseType.Code;

            //    // Configure the scope
            //    options.Scope.Add("openid");

            //    // Set the callback path, so Auth0 will call back to http://localhost:3000/callback
            //    // Also ensure that you have added the URL as an Allowed Callback URL in your Auth0 dashboard
            //    options.CallbackPath = new PathString("/callback");

            //    // Configure the Claims Issuer to be Auth0
            //    options.ClaimsIssuer = "Auth0";

            //    // Configure the scope
            //    options.Scope.Clear();
            //    options.Scope.Add("openid");
            //    options.Scope.Add("profile");
            //    options.Scope.Add("email");
            //    //     options.Scope.Add("roles");

            //    // Set the correct name claim type
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        NameClaimType = "name",
            //        RoleClaimType = "https://schemas.quickstarts.com/roles"
            //    };

            //    options.Events = new OpenIdConnectEvents
            //    {
            //        // handle the logout redirection
            //        OnRedirectToIdentityProviderForSignOut = (context) =>
            //        {
            //            var logoutUri = $"https://{Configuration["Auth0:Domain"]}/v2/logout?client_id={Configuration["Auth0:ClientId"]}";

            //            var postLogoutUri = context.Properties.RedirectUri;
            //            if (!string.IsNullOrEmpty(postLogoutUri))
            //            {
            //                if (postLogoutUri.StartsWith("/"))
            //                {
            //                    // transform to absolute
            //                    var request = context.Request;
            //                    postLogoutUri = request.Scheme + "://" + request.Host + request.PathBase + postLogoutUri;
            //                }
            //                logoutUri += $"&returnTo={ Uri.EscapeDataString(postLogoutUri)}";
            //            }

            //            context.Response.Redirect(logoutUri);
            //            context.HandleResponse();

            //            return Task.CompletedTask;
            //        },
            //        OnRemoteFailure = (ctx) =>
            //        {
            //            TelemetryConfiguration tc1 = TelemetryConfiguration.CreateDefault();
            //            tc1.InstrumentationKey = Configuration.GetValue<string>("APPINSIGHTS_INSTRUMENTATIONKEY");

            //            var client = new TelemetryClient(tc1);
            //            client.TrackException(ctx.Failure);
            //            //ai.
            //            // React to the error here. See the notes below.
            //            return Task.CompletedTask;
            //        }
            //    };
            //});


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



            // services.AddRazorPages();

            services.AddControllersWithViews(opt => opt.Filters.Add(typeof(ViewModelFilter))).AddMicrosoftIdentityUI()
                .AddRazorRuntimeCompilation().AddApplicationPart(typeof(MarkdownPageProcessorMiddleware).Assembly);

            services.AddApplicationInsightsTelemetry(Configuration["APPINSIGHTS_INSTRUMENTATIONKEY"]);
            services.AddFeatureManagement();

            services.AddSignalR(o => o.EnableDetailedErrors = true)
                .AddMessagePackProtocol().AddAzureSignalR();

            services.Configure<OpenIdConnectOptions>(Configuration.GetSection("AzureAdB2C"));
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Use((httpContext, next) =>
            {
                if (httpContext.Request.Headers["x-forwarded-proto"] == "https")
                {
                    httpContext.Request.Scheme = "https";
                }
                return next();
            });


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
            app.UseCookiePolicy();
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
                endpoints.MapHub<AuthoringHub>("/authoringHub");
            });
            app.UseAzureAppConfiguration();
        }




        private void CheckSameSite(HttpContext httpContext, CookieOptions options)
        {
            if (options.SameSite == SameSiteMode.None)
            {
                var userAgent = httpContext.Request.Headers["User-Agent"].ToString();
                if (DisallowsSameSiteNone(userAgent))
                {
                    options.SameSite = SameSiteMode.Unspecified;
                }
            }
        }


        //  Read comments in https://docs.microsoft.com/en-us/aspnet/core/security/samesite?view=aspnetcore-3.1
        public bool DisallowsSameSiteNone(string userAgent)
        {
            // Check if a null or empty string has been passed in, since this
            // will cause further interrogation of the useragent to fail.
            if (String.IsNullOrWhiteSpace(userAgent))
                return false;

            // Cover all iOS based browsers here. This includes:
            // - Safari on iOS 12 for iPhone, iPod Touch, iPad
            // - WkWebview on iOS 12 for iPhone, iPod Touch, iPad
            // - Chrome on iOS 12 for iPhone, iPod Touch, iPad
            // All of which are broken by SameSite=None, because they use the iOS networking
            // stack.
            if (userAgent.Contains("CPU iPhone OS 12") ||
                userAgent.Contains("iPad; CPU OS 12"))
            {
                return true;
            }

            // Cover Mac OS X based browsers that use the Mac OS networking stack. 
            // This includes:
            // - Safari on Mac OS X.
            // This does not include:
            // - Chrome on Mac OS X
            // Because they do not use the Mac OS networking stack.
            if (userAgent.Contains("Macintosh; Intel Mac OS X 10_14") &&
                userAgent.Contains("Version/") && userAgent.Contains("Safari"))
            {
                return true;
            }

            // Cover Chrome 50-69, because some versions are broken by SameSite=None, 
            // and none in this range require it.
            // Note: this covers some pre-Chromium Edge versions, 
            // but pre-Chromium Edge does not require SameSite=None.
            if (userAgent.Contains("Chrome/5") || userAgent.Contains("Chrome/6"))
            {
                return true;
            }

            return false;
        }
    }
}
