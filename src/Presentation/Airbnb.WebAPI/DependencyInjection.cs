using Microsoft.OpenApi.Models;

namespace Airbnb.WebAPI
{
    public static class DependencyInjection
    {
        //public static IServiceCollection GeneralDIs(this IServiceCollection services)
        //{
            
        //}
        public static IServiceCollection AddSwaggerAndCustomJwtService(this IServiceCollection services)
            {
                services.AddSwaggerGen(opt =>
                {
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
