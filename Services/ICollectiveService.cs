using Night.ViewModels;

namespace Night.Services;

public interface ICollectiveService
{
    Task<HomeIndexViewModel> GetHomePageContentAsync();

    Task<MembersPageViewModel> GetMembersPageAsync();

    Task<IReadOnlyCollection<CollectiveMemberViewModel>> GetCollectiveMembersAsync();

    Task<IReadOnlyCollection<GameViewModel>> GetGamesAsync();

    Task<IReadOnlyCollection<GameViewModel>> GetVisibleGamesAsync();

    Task<EventBasicInfoViewModel?> GetNextUpcomingEventBasicInfoAsync();

    Task AddCollectiveMemberAsync(CollectiveMemberFormViewModel viewModel);

    Task UpdateCollectiveMemberAsync(CollectiveMemberFormViewModel viewModel);

    Task DeleteCollectiveMemberAsync(int id);

    Task AddGameAsync(GameFormViewModel viewModel);

    Task UpdateGameAsync(GameFormViewModel viewModel);

    Task DeleteGameAsync(int id);

    Task<GameFormViewModel> GetGameFormAsync();

    Task<CollectiveMemberFormViewModel> GetMemberFormAsync();

    Task<DisplaySettingsViewModel> GetDisplaySettingsAsync();

    Task UpdateDisplaySettingsAsync(DisplaySettingsViewModel settings);
}
