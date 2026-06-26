using Night.ViewModels;

namespace Night.Services;

public interface ICollectiveService
{
    Task<HomeIndexViewModel> GetHomePageContentAsync();

    Task<MembersPageViewModel> GetMembersPageAsync();

    Task<IReadOnlyCollection<CollectiveMemberViewModel>> GetCollectiveMembersAsync();

    Task<MembersPageViewModel> GetMembersPageAsync();

    Task AddCollectiveMemberAsync(CollectiveMemberFormViewModel viewModel);

    Task UpdateCollectiveMemberAsync(CollectiveMemberFormViewModel viewModel);

    Task AddGameAsync(GameFormViewModel viewModel);

    Task UpdateGameAsync(GameFormViewModel viewModel);

    Task<EventBasicInfoViewModel?> GetNextUpcomingEventBasicInfoAsync();

    Task AddGameAsync(GameFormViewModel viewModel);

    Task AddCollectiveMemberAsync(CollectiveMemberFormViewModel viewModel);
}
