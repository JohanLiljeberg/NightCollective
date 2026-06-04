using Night.Models;
using Night.ViewModels;

namespace Night.Services;

public interface ICollectiveService
{
    HomeIndexViewModel GetHomePageContent();

    IReadOnlyCollection<CollectiveMember> GetCollectiveMembers();
}
