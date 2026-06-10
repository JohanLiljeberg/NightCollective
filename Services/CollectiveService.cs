using Night.Models;
using Night.Repositories;
using Night.ViewModels;

namespace Night.Services;

public class CollectiveService(ICollectiveRepository collectiveRepository) : ICollectiveService
{
    public async Task<HomeIndexViewModel> GetHomePageContentAsync()
    {
        var today = DateTime.Today;

        return new HomeIndexViewModel
        {
            FeaturedProjects = (await collectiveRepository.GetFeaturedProjectsAsync()).Select(MapProject).ToList(),
            UpcomingEvents = (await collectiveRepository.GetUpcomingEventsAsync(today)).Select(MapEvent).ToList(),
            Members = (await collectiveRepository.GetCollectiveMembersAsync()).Select(MapMember).ToList()
        };
    }

    public async Task<IReadOnlyCollection<CollectiveMemberViewModel>> GetCollectiveMembersAsync()
    {
        return (await collectiveRepository.GetCollectiveMembersAsync()).Select(MapMember).ToList();
    }

    public async Task<EventBasicInfoViewModel?> GetNextUpcomingEventBasicInfoAsync()
    {
        var collectiveEvent = await collectiveRepository.GetNextUpcomingEventAsync(DateTime.Today);

        return collectiveEvent is null ? null : MapEventBasicInfo(collectiveEvent);
    }

    private static ProjectCardViewModel MapProject(CollectiveProject project)
    {
        return new ProjectCardViewModel
        {
            Title = project.Title,
            Creator = project.Creator,
            Medium = project.Medium,
            Description = project.Description
        };
    }

    private static EventViewModel MapEvent(CollectiveEvent collectiveEvent)
    {
        return new EventViewModel
        {
            Id = collectiveEvent.Id,
            Title = collectiveEvent.Title,
            Date = collectiveEvent.Date,
            Location = collectiveEvent.Location,
            Description = collectiveEvent.Description,
            ImageUrl = collectiveEvent.ImageUrl
        };
    }

    private static EventBasicInfoViewModel MapEventBasicInfo(CollectiveEvent collectiveEvent)
    {
        return new EventBasicInfoViewModel
        {
            Id = collectiveEvent.Id,
            Title = collectiveEvent.Title,
            Date = collectiveEvent.Date
        };
    }

    private static CollectiveMemberViewModel MapMember(CollectiveMember member)
    {
        return new CollectiveMemberViewModel
        {
            Name = member.Name,
            Image = member.Image,
            Position = member.Position,
            Quote = member.Quote,
            Games = member.Games.Select(game => new GameViewModel
            {
                Title = game.Title,
                ReleaseYear = game.ReleaseYear
            }).ToList()
        };
    }
}
