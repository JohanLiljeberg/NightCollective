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
            Members = collectiveRepository.GetMembers()
        };
    }
}
