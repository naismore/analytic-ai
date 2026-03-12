using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Database;

public class AppDbContextFactory : IDesignTimeDbContextFactory<DTADbContext>
{
    public DTADbContext CreateDbContext(string[] args)
    {
        // Строим конфигурацию
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../WebApi"))
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        // Создаем options builder
        var optionsBuilder = new DbContextOptionsBuilder<DTADbContext>();
        var connectionString = configuration.GetConnectionString("PostgreSQL");

        optionsBuilder.UseNpgsql(connectionString, options =>
        {
            options.MigrationsAssembly(typeof(DTADbContext).Assembly.FullName);
            options.CommandTimeout(600);
        });

        // Включаем LegacyTimestampBehavior для Npgsql
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        return new DTADbContext(optionsBuilder.Options);
    }
}