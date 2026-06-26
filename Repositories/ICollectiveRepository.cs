using Night.Models;

namespace Night.Repositories;

public interface ICollectiveRepository
{
    Task<IReadOnlyCollection<CollectiveProject>> GetFeaturedProjectsAsync();

    Task<IReadOnlyCollection<CollectiveEvent>> GetUpcomingEventsAsync(DateTime fromDate);

    Task<CollectiveEvent?> GetNextUpcomingEventAsync(DateTime fromDate);

    Task<IReadOnlyCollection<CollectiveMember>> GetCollectiveMembersAsync();

    Task<CollectiveMember?> GetCollectiveMemberByIdAsync(int id);

    Task AddCollectiveMemberAsync(CollectiveMember member);

    Task UpdateCollectiveMemberAsync(CollectiveMember member);

    Task<IReadOnlyCollection<Game>> GetGamesAsync();

    Task<Game?> GetGameByIdAsync(int id);

    Task AddGameAsync(Game game, int collectiveMemberId);

    Task UpdateGameAsync(Game game, int collectiveMemberId);
}
