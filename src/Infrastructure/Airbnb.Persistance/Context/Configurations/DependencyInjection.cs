using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Airbnb.Persistance.Context.Configurations
{
    public static class DependencyInjection
    {
        public static async Task<WebApplication> SeedDatabase(this WebApplication webApp)
        {
            using (var scope = webApp.Services.CreateAsyncScope())
            {
                using (var appContext = scope.ServiceProvider.GetRequiredService<AirbnbDbContext>())
                {
                    try
                    {
                        await appContext.Database.MigrateAsync();
                    }
                    catch (Exception)
                    {
                        throw new Exception("Exception thrown while trying to seed database");
                    }
                }
            }
            return webApp;
        }
    }
}
