using Night.Models;
using Night.ViewModels;

namespace Night.Repositories;

public interface ICollectiveRepository
{
    Task<IReadOnlyCollection<CollectiveProject>> GetFeaturedProjectsAsync();

    Task<IReadOnlyCollection<CollectiveEvent>> GetUpcomingEventsAsync(DateTime fromDate);

    Task<CollectiveEvent?> GetNextUpcomingEventAsync(DateTime fromDate);

    Task<IReadOnlyCollection<CollectiveMember>> GetCollectiveMembersAsync();

    Task<IReadOnlyCollection<Game>> GetGamesAsync();

    Task AddGameAsync(Game game, IReadOnlyCollection<GameMemberContributionFormViewModel> contributions);

    Task UpdateGameAsync(Game game, IReadOnlyCollection<GameMemberContributionFormViewModel> contributions);

    Task DeleteGameAsync(int id);

    Task AddCollectiveMemberAsync(CollectiveMember member, IReadOnlyCollection<int> gameIds);

    Task UpdateCollectiveMemberAsync(CollectiveMember member, IReadOnlyCollection<int> gameIds);

    Task DeleteCollectiveMemberAsync(int id);
}
