using Microsoft.AspNetCore.Mvc.Rendering;
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
        var availableGames = games.Select(MapGameSelect).ToList();

        return new MembersPageViewModel
        {
            Members = members,
            Games = games,
            MemberForm = new CollectiveMemberFormViewModel
            {
                AvailableGames = availableGames
            },
            GameForm = new GameFormViewModel(),
            MemberOptions = members.Select(member => new SelectListItem(member.Name, member.Id.ToString())).ToList()
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

    public async Task AddCollectiveMemberAsync(CollectiveMemberFormViewModel viewModel)
    {
        await collectiveRepository.AddCollectiveMemberAsync(MapMemberForm(viewModel), viewModel.SelectedGameIds);
    }

    public async Task UpdateCollectiveMemberAsync(CollectiveMemberFormViewModel viewModel)
    {
        if (viewModel.Id is null)
        {
            return;
        }

        await collectiveRepository.UpdateCollectiveMemberAsync(MapMemberForm(viewModel), viewModel.SelectedGameIds);
    }

    public async Task AddGameAsync(GameFormViewModel viewModel)
    {
        await collectiveRepository.AddGameAsync(MapGameForm(viewModel));
    }

    public async Task UpdateGameAsync(GameFormViewModel viewModel)
    {
        if (viewModel.Id is null)
        {
            return;
        }

        await collectiveRepository.UpdateGameAsync(MapGameForm(viewModel));
    }

    private static ProjectCardViewModel MapProject(CollectiveProject project) => new()
    {
        Title = project.Title,
        Creator = project.Creator,
        Medium = project.Medium,
        Description = project.Description
    };

    private static EventViewModel MapEvent(CollectiveEvent collectiveEvent) => new()
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

    private static EventBasicInfoViewModel MapEventBasicInfo(CollectiveEvent collectiveEvent) => new()
    {
        Id = collectiveEvent.Id,
        Title = collectiveEvent.Title,
        Date = collectiveEvent.Date
    };

    private static CollectiveMemberViewModel MapMember(CollectiveMember member) => new()
    {
        Id = member.Id,
        Name = member.Name,
        Image = member.Image,
        Position = member.Position,
        Quote = member.Quote,
        Games = member.Games.OrderBy(game => game.Title).Select(MapGame).ToList()
    };

    private static GameViewModel MapGame(Game game) => new()
    {
        Id = game.Id,
        Title = game.Title,
        ReleaseYear = game.ReleaseYear,
        Image = game.Image,
        DeveloperPublisher = game.DeveloperPublisher,
        Platforms = game.Platforms,
        GenreGameplayType = game.GenreGameplayType,
        FromCollective = game.FromCollective,
        MemberNames = game.Members.OrderBy(member => member.Name).Select(member => member.Name).ToList()
    };

    private static GameSelectViewModel MapGameSelect(GameViewModel game) => new()
    {
        Id = game.Id,
        Title = game.Title
    };

    private static CollectiveMember MapMemberForm(CollectiveMemberFormViewModel viewModel) => new()
    {
        Id = viewModel.Id ?? 0,
        Name = viewModel.Name,
        Image = viewModel.Image,
        Position = viewModel.Position,
        Quote = viewModel.Quote
    };

    private static Game MapGameForm(GameFormViewModel viewModel) => new()
    {
        Id = viewModel.Id ?? 0,
        Title = viewModel.Title,
        Image = viewModel.Image,
        ReleaseYear = viewModel.ReleaseYear,
        DeveloperPublisher = viewModel.DeveloperPublisher,
        Platforms = viewModel.Platforms,
        GenreGameplayType = viewModel.GenreGameplayType,
        FromCollective = viewModel.FromCollective
    };
}
