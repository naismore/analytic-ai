using Infrastructure.Database;
using Infrastructure.Identity.Data;
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
                var identityDb = scope.ServiceProvider.GetRequiredService<AppIdentityDbContext>();
                db.Database.Migrate();
                identityDb.Database.Migrate();
            }
        }
    }
}
