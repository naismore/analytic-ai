using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Identity.Data;

public class AppIdentityDbContextFactory : IDesignTimeDbContextFactory<AppIdentityDbContext>
{
    public AppIdentityDbContext CreateDbContext(string[] args)
    {
        // Строим конфигурацию
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../WebApi")) // Путь к Web проекту
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        // Создаем options builder
        var optionsBuilder = new DbContextOptionsBuilder<AppIdentityDbContext>();
        var connectionString = configuration.GetConnectionString("PostgreSQL");

        optionsBuilder.UseNpgsql(connectionString, options =>
        {
            options.MigrationsAssembly(typeof(AppIdentityDbContext).Assembly.FullName);
            options.CommandTimeout(600);
        });

        return new AppIdentityDbContext(optionsBuilder.Options);
    }
}