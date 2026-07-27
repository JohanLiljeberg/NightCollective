namespace Night.Repositories;

public interface IDatabaseRepository
{
    Task MigrateAsync();
}
