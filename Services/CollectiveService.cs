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
            GameForm = new GameFormViewModel
            {
                AvailableMembers = members.Select(member => new SelectListItem(member.Name, member.Id.ToString())).ToList()
            },
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
        await collectiveRepository.AddGameAsync(MapGameForm(viewModel), viewModel.MemberContributions);
    }

    public async Task UpdateGameAsync(GameFormViewModel viewModel)
    {
        if (viewModel.Id is null)
        {
            return;
        }

        await collectiveRepository.UpdateGameAsync(MapGameForm(viewModel));
    }

    public async Task<GameFormViewModel> GetGameFormAsync()
    {
        var members = await collectiveRepository.GetCollectiveMembersAsync();

        return new GameFormViewModel
        {
            AvailableMembers = members
                .Select(member => new SelectListItem(member.Name, member.Id.ToString()))
                .ToList()
        };
    }

    public async Task<CollectiveMemberFormViewModel> GetMemberFormAsync()
    {
        var games = await collectiveRepository.GetGamesAsync();

        return new CollectiveMemberFormViewModel
        {
            AvailableGames = games
                .Select(game => new GameSelectViewModel { Id = game.Id, Title = game.Title })
                .ToList()
        };
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
        ImageSmallUrl = member.ImageSmallUrl,
        ImageMediumUrl = member.ImageMediumUrl,
        ImageLargeUrl = member.ImageLargeUrl,
        Position = member.Position,
        Quote = member.Quote,
        Games = member.Games.OrderBy(game => game.Title).Select(MapGame).ToList(),
        GameContributions = member.GameContributions
            .OrderBy(gc => gc.Game.Title)
            .Select(gc => new GameContributionViewModel
            {
                GameId = gc.GameId,
                Title = gc.Game.Title,
                Image = gc.Game.Image,
                ReleaseYear = gc.Game.ReleaseYear,
                InvolvementLevel = gc.InvolvementLevel,
                WorkAreas = gc.WorkAreas
            })
            .ToList()
    };

    private static GameViewModel MapGame(Game game) => new()
    {
        Id = game.Id,
        Title = game.Title,
        ReleaseYear = game.ReleaseYear,
        Image = game.Image,
        ImageSmallUrl = game.ImageSmallUrl,
        ImageMediumUrl = game.ImageMediumUrl,
        ImageLargeUrl = game.ImageLargeUrl,
        DeveloperPublisher = game.DeveloperPublisher,
        Platforms = game.Platforms,
        GenreGameplayType = game.GenreGameplayType,
        FromCollective = game.FromCollective,
        MemberNames = game.Members.OrderBy(member => member.Name).Select(member => member.Name).ToList(),
        MemberContributions = game.MemberContributions
            .OrderBy(mc => mc.CollectiveMember.Name)
            .Select(mc => new MemberContributionViewModel
            {
                MemberId = mc.CollectiveMemberId,
                Name = mc.CollectiveMember.Name,
                Image = mc.CollectiveMember.Image,
                InvolvementLevel = mc.InvolvementLevel,
                WorkAreas = mc.WorkAreas
            })
            .ToList()
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
