using Night.Models;
using Night.Repositories;
using Night.ViewModels;

namespace Night.Services;

public class CollectiveService(ICollectiveRepository collectiveRepository) : ICollectiveService
{
    public HomeIndexViewModel GetHomePageContent()
    {
        return new HomeIndexViewModel
        {
            FeaturedProjects = collectiveRepository.GetFeaturedProjects(),
            UpcomingEvents = collectiveRepository.GetUpcomingEvents(),
            Members = collectiveRepository.GetCollectiveMembers()
        };
    }

    public IReadOnlyCollection<CollectiveMember> GetCollectiveMembers()
    {
        return collectiveRepository.GetCollectiveMembers();
    }
}
