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
                    opt.MapType<TimeSpan>(() => new OpenApiSchema
                    {
                        Type = "string",
                        Example = new OpenApiString("00:00:00")
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
                return services;
                 }

              }
    }
