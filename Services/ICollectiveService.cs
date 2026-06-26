using Night.ViewModels;

namespace Night.Services;

public interface ICollectiveService
{
    Task<HomeIndexViewModel> GetHomePageContentAsync();

    Task<MembersPageViewModel> GetMembersPageAsync();

    Task<IReadOnlyCollection<CollectiveMemberViewModel>> GetCollectiveMembersAsync();

    Task<EventBasicInfoViewModel?> GetNextUpcomingEventBasicInfoAsync();

    Task AddGameAsync(GameFormViewModel viewModel);

    Task AddCollectiveMemberAsync(CollectiveMemberFormViewModel viewModel);
}
