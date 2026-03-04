using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInitializer
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg => cfg.AddMaps(typeof(DependencyInitializer).Assembly));
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(DependencyInitializer).Assembly);
        });
    }
}