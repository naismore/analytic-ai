using Application;
using Infrastructure;
using Infrastructure.Identity.Seed;
using Microsoft.OpenApi.Models;
using System.Reflection;
using WebApi.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "DataToolAdvisor",
        Version = "v1",
        Description = "API для подбора инструментов аналитики"
    });

    // Настройка JWT авторизации
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Введите ваш JWT токен."
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"  // Должно совпадать с названием из AddSecurityDefinition
                }
            },
            new List<string>()
        }
    });
});

builder.Services.AddAuthorization();

var applicationAssembly = typeof(DependencyInitializer).Assembly;
var webApiAssembly = Assembly.GetExecutingAssembly();
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddMaps(applicationAssembly);
    cfg.AddMaps(webApiAssembly);
});


builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();

var app = builder.Build();

app.ApplyMigrations();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        // Создаем роли
        await RoleSeeder.SeedRolesAsync(services);

    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}
app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
