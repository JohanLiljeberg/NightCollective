using Night.Repositories;

namespace Night.Services;

public class DatabaseInitializerService(IDatabaseRepository databaseRepository) : IDatabaseInitializerService
{
    public async Task MigrateAsync()
    {
        await databaseRepository.MigrateAsync();
    }
}
