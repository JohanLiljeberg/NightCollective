using Night.ViewModels;

namespace Night.Services;

public interface ICollectiveService
{
    Task<HomeIndexViewModel> GetHomePageContentAsync();

    Task<IReadOnlyCollection<CollectiveMemberViewModel>> GetCollectiveMembersAsync();

    Task<EventBasicInfoViewModel?> GetNextUpcomingEventBasicInfoAsync();
}
