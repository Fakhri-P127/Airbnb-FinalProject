using Airbnb.Application.Middlewares;
using Airbnb.Persistance.Email;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace Airbnb.WebAPI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSwaggerAndConfigureJwtService(this IServiceCollection services)
        {
            services.AddSwaggerGen(opt =>
            {
                //configure timespan for checkInTime and checkOutTime
                opt.MapType<TimeSpan>(() => new OpenApiSchema
                {
                    Type = "string",
                    Example = new OpenApiString(string.Empty)
                });
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Description = "Bearer Authentication with JWT Token",
                    Type = SecuritySchemeType.Http
                });

                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                 {
            new OpenApiSecurityScheme
                    {
                Reference= new OpenApiReference
                      {
                    Id="Bearer",
                    Type=ReferenceType.SecurityScheme
                         }
                  },
            new List<string>()
                      }
              });
            })
                .AddApiVersioningAndApiExplorer()
                .AddHttpClient()
                // singletion olanda gerek serveri saxlayib tezden giresen(ya da tezden login).
            .AddSingleton<IAuthorizationMiddlewareResultHandler, MyAuthorizationMiddlewareResultHandler>();
            return services;
        }
        public static IServiceCollection AddApiVersioningAndApiExplorer(this IServiceCollection services)
        {
            services.AddApiVersioning(opt =>
            {
                opt.ReportApiVersions = true;
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.DefaultApiVersion = new ApiVersion(1, 0);
                opt.ApiVersionReader = new UrlSegmentApiVersionReader();

            });
            services.AddVersionedApiExplorer(opt => opt.GroupNameFormat = "'v'VVV");
            return services;
        }

    }
}
