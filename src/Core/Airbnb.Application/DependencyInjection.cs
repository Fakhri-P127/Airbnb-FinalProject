using Airbnb.Application.Common.CustomFrameworkImpl;
using Airbnb.Application.PiplineBehaviours;
using FluentValidation;
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

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPiplineBehaviour<,>));
            services.AddScoped(typeof(CustomUserManager<>));

            return services;
        }
    }
}
