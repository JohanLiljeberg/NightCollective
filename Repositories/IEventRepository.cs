using Night.Models;

namespace Night.Repositories;

public interface IEventRepository
{
    Task<IReadOnlyCollection<CollectiveEvent>> GetUpcomingAsync(DateTime fromDate);

    Task<CollectiveEvent?> GetByIdAsync(int id);

    Task AddAsync(CollectiveEvent collectiveEvent);

    Task UpdateAsync(CollectiveEvent collectiveEvent);

    Task DeleteAsync(int id);
}
