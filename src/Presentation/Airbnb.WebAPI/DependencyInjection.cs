using Airbnb.Application.Middlewares;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using System.Reflection;

namespace Airbnb.WebAPI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebApiDI(this IServiceCollection services, ConfigureHostBuilder host)
        {
            //services.AddEndpointsApiExplorer();// bu minimal API uchundu, silmek olar
            services.AddRouting(x => x.LowercaseUrls = true);

            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1",new OpenApiInfo { Title = "Airbnb API", Version = "v1" });
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

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                opt.IncludeXmlComments(xmlPath);
                opt.DescribeAllParametersInCamelCase();

                //configure timespan for checkInTime and checkOutTime
                opt.MapType<TimeSpan>(() => new OpenApiSchema
                {
                    Type = "string",
                    Example = new OpenApiString(string.Empty)
                });
            })
                .AddApiVersioningAndApiExplorer()
                .AddHttpClient()
            .AddSingleton<IAuthorizationMiddlewareResultHandler, MyAuthorizationMiddlewareResultHandler>();

            //host.UseSerilog((hostingContext, loggerConfiguration) =>
            //{
            //    loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration).WriteTo
            //    .Console(theme: SystemConsoleTheme.Colored);
            //});
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
            })
            .AddVersionedApiExplorer(opt => opt.GroupNameFormat = "'v'VVV");
            return services;
        }

        public static IApplicationBuilder CustomRun(this WebApplication app)
        {
            try
            {
                Log.Information("Application Starting.");
                app.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "The Application failed to start.");
            }
            finally
            {
                Log.CloseAndFlush();
            }
            return app;
        }
    }
}
