using Airbnb.Application;
using Airbnb.Application.Filters.ActionFilters;
using Airbnb.Application.Filters.ResourceFilters;
using Airbnb.Persistance;
using Airbnb.WebAPI;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;


var builder = WebApplication.CreateBuilder(args);
Log.Logger = (Serilog.ILogger)new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration).CreateLogger();
    //.MinimumLevel.Debug()
    //.MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    //.Enrich.FromLogContext()
    //.WriteTo.File(@"C:\Users\efend\Desktop\Loggings2.txt")

// Add services to the container.

builder.Services.AddControllers(config =>
{
    config.Filters.Add<EnsureIdIsGuidResourceFilter>();
    config.Filters.Add<EnsureEnteredUserIdIsSameWithAuthenticatedUserId_ActionFilterAttribute>();
});
//.AddJsonOptions(opt => opt.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddApplicationDI();
builder.Services.AddInfrastructureDI(builder.Configuration);
builder.Services.AddWebApiDI(builder.Host);
builder.Host.UseSerilog();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler("/error");
//await app.SeedDatabase();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.CustomRun();
