using Night.Models;

namespace Night.Repositories;

public interface ICollectiveRepository
{
    IReadOnlyCollection<CollectiveProject> GetFeaturedProjects();

    IReadOnlyCollection<CollectiveEvent> GetUpcomingEvents();

    IReadOnlyCollection<Developer> GetMembers();
}
