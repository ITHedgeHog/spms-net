using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.FileProviders;
using SPMS.Application.Dtos;
using SPMS.Web.Infrastructure.Middlware;

namespace SPMS.Web.Infrastructure.Extensions
{
    /// <summary>
    /// Nice method to register our middleware
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public static class IApplicationBuilderExtensions
    {
        /// <summary>
        /// Use the Tenant Middleware to process the request
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseMultiTenancy<T>(this IApplicationBuilder builder) where T : TenantDto
            => builder.UseMiddleware<TenantMiddleware<T>>();


        /// <summary>
        /// Use the Tenant Middleware to process the request
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseMultiTenancy(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<TenantMiddleware<TenantDto>>();
            return builder;
        }

        public static IApplicationBuilder UsePerTenantStaticFiles(
            this IApplicationBuilder app,
            IWebHostEnvironment env)
        {
            //0ar routeTemplate = "/" + pathPrefix + "/{**filePath}";
          
            app.MapWhen(
                context => context.Request.Path.ToString().StartsWith("/tenant"),
                fork => {
                    fork.UseMiddleware<TenantFilesMiddleware>();
                    fork.UseStaticFiles(new StaticFileOptions
                    {
                        FileProvider = new PhysicalFileProvider(env.ContentRootPath + "/tenantfiles"),
                        RequestPath = "/tenantfiles",
                    });
                });
            //app.Map(routeTemplate, (IApplicationBuilder fork) =>
            //{
            //    fork.UseMiddleware<TenantFilesMiddleware>(pathPrefix);
            //    fork.UseStaticFiles();
            //});
            //var router = routeBuilder.Build();
            //app.UseRouter(router);

            return app;
        }


        public static IApplicationBuilder UseThemeStaticFiles(
            this IApplicationBuilder app,
            IWebHostEnvironment env)
        {
            app.MapWhen(
                context => context.Request.Path.ToString().StartsWith("/theme"),
                fork =>
                {
                    fork.UseMiddleware<TenantThemeMiddleware>();
                    fork.UseStaticFiles(new StaticFileOptions
                    {
                        FileProvider = new PhysicalFileProvider(env.ContentRootPath + "/Themes"),
                        RequestPath = "/tenanttheme",
                    });
                });
            return app;
        }

    }

    public static class EndpointRouteBuilderExtensions
    {
        public static IEndpointConventionBuilder MapTenantFiles(
            this IEndpointRouteBuilder endpoints, IWebHostEnvironment env)
        {
            var pipeline = endpoints.CreateApplicationBuilder()
                .UseMiddleware<TenantFilesMiddleware>()
                .UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(env.ContentRootPath + "/tenantfiles"),
                    RequestPath = "/tenant",
                })
                .Build();

            return endpoints.MapGet("/tenant/{**file}", pipeline);
        }
    }
}
