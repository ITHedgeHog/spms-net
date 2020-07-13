using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SPMS.Application.Common.Interfaces;
using SPMS.Application.Common.Provider;
using SPMS.Application.Services;

namespace SPMS.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            services.AddScoped<IGameService, GameService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IStoryService, StoryService>();
            services.AddScoped<IAuthoringService, AuthoringService>();
            services.AddScoped<IIdentifierMask, IdentifierMasking>();
            services.AddScoped<ITenantProvider, TenantProvider>();

            return services;
        }
    }
}
