using Microsoft.EntityFrameworkCore;
using Night.Data;
using Night.Repositories;
using Night.Services;

namespace Night.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddNightCollectiveServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("NightCollectiveDatabase")
            ?? configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'NightCollectiveDatabase' or 'DefaultConnection' was not found.");

        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
        services.AddScoped<ICollectiveRepository, SqlCollectiveRepository>();
        services.AddScoped<IEventRepository, SqlEventRepository>();
        services.AddScoped<IDatabaseRepository, SqlDatabaseRepository>();
        services.AddScoped<ICollectiveService, CollectiveService>();
        services.AddScoped<IEventService, EventService>();
        services.AddScoped<IDatabaseInitializerService, DatabaseInitializerService>();

        return services;
    }
}
