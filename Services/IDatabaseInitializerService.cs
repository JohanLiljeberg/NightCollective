namespace Night.Services;

public interface IDatabaseInitializerService
{
    Task MigrateAsync();
}
