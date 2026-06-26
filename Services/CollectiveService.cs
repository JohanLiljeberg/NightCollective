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

    public async Task<MembersPageViewModel> GetMembersPageAsync()
    {
        var members = (await collectiveRepository.GetCollectiveMembersAsync()).Select(MapMember).ToList();
        var games = (await collectiveRepository.GetGamesAsync()).Select(MapGame).ToList();

        return new MembersPageViewModel
        {
            Members = members,
            Games = games,
            MemberForm = new CollectiveMemberFormViewModel
            {
                AvailableGames = games.Select(MapGameSelect).ToList()
            },
            GameForm = new GameFormViewModel()
        };
    }

    public async Task<IReadOnlyCollection<CollectiveMemberViewModel>> GetCollectiveMembersAsync()
    {
        return (await collectiveRepository.GetCollectiveMembersAsync()).Select(MapMember).ToList();
    }

    public async Task AddGameAsync(GameFormViewModel viewModel)
    {
        var game = new Game
        {
            Title = viewModel.Title,
            Image = viewModel.Image,
            ReleaseYear = viewModel.ReleaseYear,
            DeveloperPublisher = viewModel.DeveloperPublisher,
            Platforms = viewModel.Platforms,
            GenreGameplayType = viewModel.GenreGameplayType,
            FromCollective = viewModel.FromCollective
        };

        await collectiveRepository.AddGameAsync(game);
    }

    public async Task AddCollectiveMemberAsync(CollectiveMemberFormViewModel viewModel)
    {
        var member = new CollectiveMember
        {
            Name = viewModel.Name,
            Image = viewModel.Image,
            Position = viewModel.Position,
            Quote = viewModel.Quote
        };

        await collectiveRepository.AddCollectiveMemberAsync(member, viewModel.SelectedGameIds);
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
            ImageSmallUrl = collectiveEvent.ImageSmallUrl ?? string.Empty,
            ImageMediumUrl = collectiveEvent.ImageMediumUrl ?? string.Empty,
            ImageLargeUrl = collectiveEvent.ImageLargeUrl ?? string.Empty
        };
    }

    private static GameViewModel MapGame(Game game)
    {
        return new GameViewModel
        {
            Id = game.Id,
            Title = game.Title,
            ReleaseYear = game.ReleaseYear,
            Image = game.Image,
            DeveloperPublisher = game.DeveloperPublisher
        };
    }

    private static GameSelectViewModel MapGameSelect(GameViewModel game)
    {
        return new GameSelectViewModel
        {
            Id = game.Id,
            Title = game.Title
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
            Games = member.Games.Select(MapGame).ToList()
        };
    }
}
