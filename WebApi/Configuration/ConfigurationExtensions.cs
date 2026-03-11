using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Configuration
{
    public static class ConfigurationExtensions
    {
        public static void ApplyMigrations(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<DTADbContext>();
                db.Database.Migrate();
            }
        }
    }
}
