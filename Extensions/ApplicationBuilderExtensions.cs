using Night.Services;

namespace Night.Extensions;

public static class ApplicationBuilderExtensions
{
    public static async Task EnsureNightCollectiveDatabaseAsync(this WebApplication app)
    {
        if (!app.Configuration.GetValue("Database:ApplyMigrationsOnStartup", true))
        {
            return;
        }

        await using var scope = app.Services.CreateAsyncScope();
        var databaseInitializerService = scope.ServiceProvider.GetRequiredService<IDatabaseInitializerService>();
        await databaseInitializerService.MigrateAsync();
    }
}
