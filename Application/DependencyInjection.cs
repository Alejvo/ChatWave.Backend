using Application.Behaviors;
using Application.Hubs;
using Application.Services;
using Domain.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblyContaining<ApplicationAssemblyReference>();
            });
            services.AddSignalR();
            services.AddScoped(
                typeof(IPipelineBehavior<,>),
                typeof(ValidationBehavior<,>));

            services.AddScoped<IAuthService,AuthService>();

            services.AddValidatorsFromAssemblyContaining<ApplicationAssemblyReference>();

            services.AddSingleton<IUserIdProvider,AppUserIdProvider>();
            return services;
        }
    }
}
