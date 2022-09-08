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
            services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssembly(assembly);

            services.AddMediatR(assembly);
            services.AddAutoMapper(assembly);
         

            return services;
        }
    }
}
