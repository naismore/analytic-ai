using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInitializer
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(DependencyInitializer).Assembly);
        });
    }
}