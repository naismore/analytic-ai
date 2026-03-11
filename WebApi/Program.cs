using Application;
using Infrastructure;
using System.Reflection;
using WebApi.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
