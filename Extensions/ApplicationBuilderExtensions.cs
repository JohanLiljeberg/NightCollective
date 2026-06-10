using Microsoft.EntityFrameworkCore;
using Night.Data;

namespace Night.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void EnsureNightCollectiveDatabase(this WebApplication app)
    {
        if (!app.Configuration.GetValue("Database:ApplyMigrationsOnStartup", true))
        {
            return;
        }

        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        dbContext.Database.Migrate();
    }
}
