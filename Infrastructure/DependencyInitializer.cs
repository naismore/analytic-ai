using Application.Abstract;
using Domain.Interfaces;
using Infrastructure.Authentication.Repositories;
using Infrastructure.Authentication.Services;
using Infrastructure.Database;
using Infrastructure.Identity;
using Infrastructure.Identity.Data;
using Infrastructure.Identity.Models;
using Infrastructure.Identity.Services;
using Infrastructure.Identity.Settings;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;

//using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
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

        services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
        {
            IdentityExtensions.ConfigureIdentityOptions(options, identitySettings); // Конфигурируем identity с помощью appsettings.json
        })
        .AddEntityFrameworkStores<AppIdentityDbContext>(); // Регистрируем в DI-контейнере готовые репозитории для работы с User и Roles

        // Регистрируем в зависимости наш контекст для базы данных
        services.AddDbContext<DTADbContext>(options => options.UseNpgsql(connString, assembly =>
        {
            assembly.MigrationsAssembly("Infrastructure"); // Указываем EF Core, где искать миграции
            assembly.CommandTimeout(600); // Задержка между командами
        }));

        // JWT Section
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

        // Если нужен экземпляр напрямую
        services.AddSingleton(sp => sp.GetRequiredService<IOptions<JwtSettings>>().Value);

        // TokenValidationParameters
        var jwtSettings = configuration.GetSection(JwtSettings.SectionName).Get<JwtSettings>();
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
            ValidateIssuer = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidateAudience = true,
            ValidAudience = jwtSettings.Audience,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            NameClaimType = ClaimTypes.NameIdentifier,
            RoleClaimType = ClaimTypes.Role
        };
        services.AddSingleton(tokenValidationParameters);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }) 
        .AddJwtBearer(options => // TODO: Это тоже думаю можно вынести в отдельный метод
        {
            options.TokenValidationParameters = tokenValidationParameters;

            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    var accessToken = context.Request.Query["access_token"];
                    var path = context.HttpContext.Request.Path;

                    if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hubs"))
                    {
                        context.Token = accessToken;
                    }

                    return Task.CompletedTask;
                }
            };
        });

        services.AddScoped<IAnalyticsToolRepository, AnalyticsToolRepository>();
        services.AddScoped<IRecommendationResultRepository, RecommendationResultRepository>();
        services.AddScoped<IRecommendationSessionRepository, RecommendationSessionRepository>();
        services.AddScoped<IRecommendationAttributesRepository, RecommendationAttributesRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IAccessTokenService, AccessTokenService>();
        services.AddScoped<IUserIdentityService, UserIdentityService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IRefreshTokenService, RefreshTokenService>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
    }
}