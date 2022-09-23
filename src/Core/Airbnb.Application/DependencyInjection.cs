using Airbnb.Application.PiplineBehaviours;
using Airbnb.Domain.Entities.AppUserRelated;
using Airbnb.Persistance.Authentication.CustomFrameworkClasses;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;



namespace Airbnb.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationDI(this IServiceCollection services)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            services.AddValidatorsFromAssembly(assembly)
                .AddMediatR(assembly)
                .AddAutoMapper(assembly)
                .AddHttpContextAccessor();

            services.AddScoped(typeof(CustomUserManager<>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPiplineBehaviour<,>));


            return services;
        }
    }
}
