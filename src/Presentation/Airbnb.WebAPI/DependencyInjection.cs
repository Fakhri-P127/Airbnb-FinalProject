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
                    Example = new OpenApiString("00:00")////////check if this works
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
            });
            //AddApiVersioningAndApiExplorer(services);
            return services;
        }
        public static IServiceCollection AddApiVersioningAndApiExplorer(this IServiceCollection services)
        {
            services.AddApiVersioning(opt =>
            {
                opt.ReportApiVersions = true;
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.DefaultApiVersion = new ApiVersion(1, 0);
                opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                                                                new HeaderApiVersionReader("x-api-version"),
                                                                new MediaTypeApiVersionReader("x-api-version"));

                //opt.ApiVersionReader = new HeaderApiVersionReader("X-API-VERSION");
            });
            services.AddVersionedApiExplorer(opt => opt.GroupNameFormat = "'v'VVV");
            return services;
        }
    }
}
