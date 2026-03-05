//using Application;
using Application;
using Infrastructure;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthorization();
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
//    {
//        options.TokenValidationParameters = new()
//        {
//            ValidateIssuer = false,
//            ValidateAudience = false,
//            ValidateLifetime = true,
//            ValidateIssuerSigningKey = true,
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes()),
//        };

//        options.Events = new JwtBearerEvents
//        {
//            OnMessageReceived = context =>
//            {
//                context.Token = context.Request.Cookies["access_token"];
//                return Task.CompletedTask;
//            }
//        };
//    });

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

//app.ApplyMigrations();

//await app.CreateIdentityRoles();

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
