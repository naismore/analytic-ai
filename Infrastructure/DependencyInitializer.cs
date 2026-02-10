using Domain.Interfaces;
using Infrastructure.Database;
using Infrastructure.Repositories;
//using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Infrastructure;
public class DependencyInititalizer
{
    public static void AddInfrastructureServices(IServiceCollection services, IConfiguration configuration)
    {
        // Получаем строку подключения из файла Web/appsettings.json через configuration
        var connString = configuration.GetConnectionString("PostgreSQL");

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