using Night.Models;

namespace Night.Repositories;

public interface ICollectiveRepository
{
    Task<IReadOnlyCollection<CollectiveProject>> GetFeaturedProjectsAsync();

    Task<IReadOnlyCollection<CollectiveEvent>> GetUpcomingEventsAsync(DateTime fromDate);

    Task<CollectiveEvent?> GetNextUpcomingEventAsync(DateTime fromDate);

    Task<IReadOnlyCollection<CollectiveMember>> GetCollectiveMembersAsync();
}
