using Microsoft.EntityFrameworkCore;
using Night.Data;

namespace Night.Repositories;

public class SqlDatabaseRepository(AppDbContext dbContext) : IDatabaseRepository
{
    public async Task MigrateAsync()
    {
        await dbContext.Database.MigrateAsync();
    }
}
