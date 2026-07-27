using Night.ViewModels;

namespace Night.Services;

public interface ICollectiveService
{
    Task<HomeIndexViewModel> GetHomePageContentAsync();

    Task<MembersPageViewModel> GetMembersPageAsync();

    Task<IReadOnlyCollection<CollectiveMemberViewModel>> GetCollectiveMembersAsync();

    Task<EventBasicInfoViewModel?> GetNextUpcomingEventBasicInfoAsync();

    Task AddCollectiveMemberAsync(CollectiveMemberFormViewModel viewModel);

    Task UpdateCollectiveMemberAsync(CollectiveMemberFormViewModel viewModel);

    Task AddGameAsync(GameFormViewModel viewModel);

    Task UpdateGameAsync(GameFormViewModel viewModel);
}
