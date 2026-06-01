using Night.Repositories;
using Night.Services;

namespace Night.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddNightCollectiveServices(this IServiceCollection services)
    {
        services.AddSingleton<ICollectiveRepository, InMemoryCollectiveRepository>();
        services.AddScoped<ICollectiveService, CollectiveService>();

        return services;
    }
}
