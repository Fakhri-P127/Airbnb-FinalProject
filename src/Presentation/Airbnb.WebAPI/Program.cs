using Airbnb.Application;
using Airbnb.Application.Filters.ResourceFilters;
using Airbnb.Persistance;
using Airbnb.WebAPI;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(config=>config.Filters.Add<EnsureIdIsGuidResourceFilter>());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationDI();
builder.Services.AddInfrastructureDI(builder.Configuration);
builder.Services.AddSwaggerAndCustomJwtService();
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseExceptionHandler("/error");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
