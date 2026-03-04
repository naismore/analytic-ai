using Domain.Interfaces;
using Infrastructure.Database;
using Infrastructure.Identity;
using Infrastructure.Identity.Data;
using Infrastructure.Identity.Models;
using Infrastructure.Identity.Settings;
using Infrastructure.Repositories;
//using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Infrastructure;
public static class DependencyInititalizer
{
    public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connString = configuration.GetConnectionString("PostgreSQL");
        var identitySettings = new IdentitySettings();

        configuration.GetSection(IdentitySettings.SectionName).Bind(identitySettings); // Вытаскиваем настройки Identity из appsettings.json и преобразуем их в IdentitySettings

        services.AddDbContext<AppIdentityDbContext>(options => options.UseNpgsql(connString));

        services.AddIdentityCore<ApplicationUser>(options =>
        {
            IdentityExtensions.ConfigureIdentityOptions(options, identitySettings); // Конфигурируем identity с помощью appsettings.json
        })
        .AddRoles<ApplicationRole>() // Добавляем роли
        .AddEntityFrameworkStores<AppIdentityDbContext>(); // Регистрируем в DI-контейнере готовые репозитории для работы с User и Roles

        // Регистрируем в зависимости наш контекст для базы данных
        services.AddDbContext<DTADbContext>(options => options.UseNpgsql(connString, assembly =>
        {
            assembly.MigrationsAssembly("Infrastructure"); // Указываем EF Core, где искать миграции
            assembly.CommandTimeout(600); // Задержка между командами
        }));

        services.AddScoped<IAnalyticsToolRepository, AnalyticsToolRepository>();
        services.AddScoped<IConversationRepository, ConversationRepository>();
        services.AddScoped<IMessageRepository, MessageRepository>();
        services.AddScoped<IRecommendationResultRepository, RecommendationResultRepository>();
        services.AddScoped<IRecommendationSessionRepository, RecommendationSessionRepository>();
        services.AddScoped<IUserProfileRepository, UserProfileRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
    }
}