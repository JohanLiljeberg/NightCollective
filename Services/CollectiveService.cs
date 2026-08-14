using Microsoft.AspNetCore.Mvc.Rendering;
using Night.Models;
using Night.Repositories;
using Night.ViewModels;

namespace Night.Services;

public class CollectiveService(ICollectiveRepository collectiveRepository, IBlogPostService blogPostService, IImageService imageService) : ICollectiveService
{
    public async Task<HomeIndexViewModel> GetHomePageContentAsync()
    {
        var today = DateTime.Today;
        var settings = await collectiveRepository.GetDisplaySettingsAsync();

        return new HomeIndexViewModel
        {
            FeaturedProjects = (await collectiveRepository.GetFeaturedProjectsAsync()).Select(MapProject).ToList(),
            LatestBlogPosts = await blogPostService.GetLatestPublishedAsync(3),
            UpcomingEvents = (await collectiveRepository.GetUpcomingEventsAsync(today)).Select(MapEvent).ToList(),
            Members = (await collectiveRepository.GetCollectiveMembersAsync())
                .Where(member => IsMemberVisible(member, settings))
                .Select(MapMember)
                .ToList()
        };
    }

    public async Task<MembersPageViewModel> GetMembersPageAsync()
    {
        var settings = await collectiveRepository.GetDisplaySettingsAsync();

        var members = (await collectiveRepository.GetCollectiveMembersAsync())
            .Where(member => IsMemberVisible(member, settings))
            .Select(MapMember)
            .ToList();

        var games = (await collectiveRepository.GetGamesAsync())
            .Where(game => IsGameVisible(game, settings))
            .Select(MapGame)
            .ToList();

        return new MembersPageViewModel
        {
            Members = members,
            Games = games
        };
    }

    public async Task<IReadOnlyCollection<CollectiveMemberViewModel>> GetCollectiveMembersAsync()
    {
        return (await collectiveRepository.GetCollectiveMembersAsync()).Select(MapMember).ToList();
    }

    public async Task<IReadOnlyCollection<GameViewModel>> GetGamesAsync()
    {
        return (await collectiveRepository.GetGamesAsync()).Select(MapGame).ToList();
    }

    public async Task<IReadOnlyCollection<GameViewModel>> GetVisibleGamesAsync()
    {
        var settings = await collectiveRepository.GetDisplaySettingsAsync();

        return (await collectiveRepository.GetGamesAsync())
            .Where(game => IsGameVisible(game, settings))
            .Select(MapGame)
            .ToList();
    }

    public async Task<EventBasicInfoViewModel?> GetNextUpcomingEventBasicInfoAsync()
    {
        var collectiveEvent = await collectiveRepository.GetNextUpcomingEventAsync(DateTime.Today);

        return collectiveEvent is null ? null : MapEventBasicInfo(collectiveEvent);
    }

    public async Task AddCollectiveMemberAsync(CollectiveMemberFormViewModel viewModel)
    {
        var member = await MapMemberForm(viewModel);
        await collectiveRepository.AddCollectiveMemberAsync(member, viewModel.SelectedGameIds, viewModel.GameContributions);
    }

    public async Task UpdateCollectiveMemberAsync(CollectiveMemberFormViewModel viewModel)
    {
        if (viewModel.Id is null)
        {
            return;
        }

        var member = await MapMemberForm(viewModel);
        await collectiveRepository.UpdateCollectiveMemberAsync(member, viewModel.SelectedGameIds, viewModel.GameContributions);
    }

    public async Task DeleteCollectiveMemberAsync(int id)
    {
        await collectiveRepository.DeleteCollectiveMemberAsync(id);
    }

    public async Task AddGameAsync(GameFormViewModel viewModel)
    {
        var game = await MapGameForm(viewModel);
        await collectiveRepository.AddGameAsync(game, viewModel.MemberContributions);
    }

    public async Task UpdateGameAsync(GameFormViewModel viewModel)
    {
        if (viewModel.Id is null)
        {
            return;
        }

        var game = await MapGameForm(viewModel);
        await collectiveRepository.UpdateGameAsync(game, viewModel.MemberContributions);
    }

    public async Task DeleteGameAsync(int id)
    {
        await collectiveRepository.DeleteGameAsync(id);
    }

    public async Task<GameFormViewModel> GetGameFormAsync()
    {
        var members = await collectiveRepository.GetCollectiveMembersAsync();

        return new GameFormViewModel
        {
            AvailableMembers = members
                .OrderByDescending(member => member.MembershipType)
                .ThenBy(member => member.Name)
                .Select(MapMemberSelect)
                .ToList()
        };
    }

    public async Task<CollectiveMemberFormViewModel> GetMemberFormAsync()
    {
        var games = await collectiveRepository.GetGamesAsync();

        return new CollectiveMemberFormViewModel
        {
            AvailableGames = games
                .OrderByDescending(game => game.FromCollective)
                .ThenBy(game => game.Title)
                .Select(MapGameSelect)
                .ToList()
        };
    }

    public async Task<DisplaySettingsViewModel> GetDisplaySettingsAsync()
    {
        var settings = await collectiveRepository.GetDisplaySettingsAsync();

        return new DisplaySettingsViewModel
        {
            ShowFullMembers = settings.ShowFullMembers,
            ShowSubscribedMembers = settings.ShowSubscribedMembers,
            ShowUnsubscribedMembers = settings.ShowUnsubscribedMembers,
            ShowCollectiveGames = settings.ShowCollectiveGames,
            ShowExternalGames = settings.ShowExternalGames
        };
    }

    public async Task UpdateDisplaySettingsAsync(DisplaySettingsViewModel settings)
    {
        await collectiveRepository.UpdateDisplaySettingsAsync(new SiteDisplaySettings
        {
            Id = 1,
            ShowFullMembers = settings.ShowFullMembers,
            ShowSubscribedMembers = settings.ShowSubscribedMembers,
            ShowUnsubscribedMembers = settings.ShowUnsubscribedMembers,
            ShowCollectiveGames = settings.ShowCollectiveGames,
            ShowExternalGames = settings.ShowExternalGames
        });
    }

    private static bool IsMemberVisible(CollectiveMember member, SiteDisplaySettings settings) => member.MembershipType switch
    {
        MembershipType.Full => settings.ShowFullMembers,
        MembershipType.Subscribed => settings.ShowSubscribedMembers,
        MembershipType.Unsubscribed => settings.ShowUnsubscribedMembers,
        _ => true
    };

    private static bool IsGameVisible(Game game, SiteDisplaySettings settings) =>
        game.FromCollective ? settings.ShowCollectiveGames : settings.ShowExternalGames;

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
        MembershipType = member.MembershipType,
        FeaturedGame = member.FeaturedGame is not null ? MapGame(member.FeaturedGame) : null,
        Games = member.Games.OrderBy(game => game.Title).Select(MapGame).ToList(),
        GameContributions = member.GameContributions
            .OrderBy(gc => gc.Game.Title)
            .Select(gc => new GameContributionViewModel
            {
                GameId = gc.GameId,
                Title = gc.Game.Title,
                Image = gc.Game.Image,
                ImageSmallUrl = gc.Game.ImageSmallUrl,
                ImageMediumUrl = gc.Game.ImageMediumUrl,
                ImageLargeUrl = gc.Game.ImageLargeUrl,
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
        Description = game.Description,
        YouTubeTrailerUrl = game.YouTubeTrailerUrl,
        Screenshots = game.Screenshots
            .OrderBy(s => s.DisplayOrder)
            .Select(s => new GameScreenshotViewModel
            {
                Id = s.Id,
                ImageSmallUrl = s.ImageSmallUrl,
                ImageMediumUrl = s.ImageMediumUrl,
                ImageLargeUrl = s.ImageLargeUrl,
                DisplayOrder = s.DisplayOrder
            })
            .ToList(),
        MemberNames = game.Members.OrderBy(member => member.Name).Select(member => member.Name).ToList(),
        MemberContributions = game.MemberContributions
            .OrderBy(mc => mc.CollectiveMember.Name)
            .Select(mc => new MemberContributionViewModel
            {
                MemberId = mc.CollectiveMemberId,
                Name = mc.CollectiveMember.Name,
                Image = mc.CollectiveMember.Image,
                ImageSmallUrl = mc.CollectiveMember.ImageSmallUrl,
                ImageMediumUrl = mc.CollectiveMember.ImageMediumUrl,
                ImageLargeUrl = mc.CollectiveMember.ImageLargeUrl,
                InvolvementLevel = mc.InvolvementLevel,
                WorkAreas = mc.WorkAreas
            })
            .ToList()
    };

    private static GameSelectViewModel MapGameSelect(Game game) => new()
    {
        Id = game.Id,
        Title = game.Title,
        ImageUrl = game.ImageSmallUrl ?? game.Image,
        FromCollective = game.FromCollective
    };

    private static MemberSelectViewModel MapMemberSelect(CollectiveMember member) => new()
    {
        Id = member.Id,
        Name = member.Name,
        ImageUrl = member.ImageSmallUrl ?? member.Image,
        MembershipType = member.MembershipType
    };

    private async Task<CollectiveMember> MapMemberForm(CollectiveMemberFormViewModel viewModel)
    {
        string? smallUrl = null;
        string? mediumUrl = null;
        string? largeUrl = null;
        string? legacyImage = viewModel.Image;

        if (viewModel.ImageFile is not null && viewModel.ImageFile.Length > 0)
        {
            // File upload takes precedence
            var sizes = await imageService.UploadImageAsync(viewModel.ImageFile, ImageType.Members);
            if (sizes is not null)
            {
                smallUrl = sizes.SmallUrl;
                mediumUrl = sizes.MediumUrl;
                largeUrl = sizes.LargeUrl;
                legacyImage = sizes.MediumUrl; // Set legacy field to medium URL
            }
        }
        else if (!string.IsNullOrWhiteSpace(viewModel.ImageUrl))
        {
            // Download URL and process into 3 sizes
            var sizes = await imageService.DownloadAndProcessUrlAsync(viewModel.ImageUrl, ImageType.Members);
            if (sizes is not null)
            {
                smallUrl = sizes.SmallUrl;
                mediumUrl = sizes.MediumUrl;
                largeUrl = sizes.LargeUrl;
                legacyImage = sizes.MediumUrl; // Set legacy field to medium URL
            }
        }

        return new CollectiveMember
        {
            Id = viewModel.Id ?? 0,
            Name = viewModel.Name,
            Image = legacyImage ?? string.Empty,
            ImageSmallUrl = smallUrl,
            ImageMediumUrl = mediumUrl,
            ImageLargeUrl = largeUrl,
            Position = viewModel.Position,
            Quote = viewModel.Quote,
            MembershipType = viewModel.MembershipType,
            FeaturedGameId = viewModel.FeaturedGameId
        };
    }

    private async Task<Game> MapGameForm(GameFormViewModel viewModel)
    {
        string? smallUrl = viewModel.ExistingImageSmallUrl;
        string? mediumUrl = viewModel.ExistingImageMediumUrl;
        string? largeUrl = viewModel.ExistingImageLargeUrl;
        string? legacyImage = viewModel.Image;

        if (viewModel.ImageFile is not null && viewModel.ImageFile.Length > 0)
        {
            // File upload takes precedence
            var sizes = await imageService.UploadImageAsync(viewModel.ImageFile, ImageType.Games);
            if (sizes is not null)
            {
                smallUrl = sizes.SmallUrl;
                mediumUrl = sizes.MediumUrl;
                largeUrl = sizes.LargeUrl;
                legacyImage = sizes.MediumUrl; // Set legacy field to medium URL
            }
        }
        else if (!string.IsNullOrWhiteSpace(viewModel.ImageUrl))
        {
            // Download URL and process into 3 sizes
            var sizes = await imageService.DownloadAndProcessUrlAsync(viewModel.ImageUrl, ImageType.Games);
            if (sizes is not null)
            {
                smallUrl = sizes.SmallUrl;
                mediumUrl = sizes.MediumUrl;
                largeUrl = sizes.LargeUrl;
                legacyImage = sizes.MediumUrl; // Set legacy field to medium URL
            }
        }
        else if (viewModel.RemoveImage)
        {
            // Admin explicitly removed the image without providing a replacement
            smallUrl = null;
            mediumUrl = null;
            largeUrl = null;
            legacyImage = string.Empty;
        }

        // Process screenshots (up to 3 new uploads, preserving existing ones not marked for removal)
        var screenshots = new List<GameScreenshot>();

        foreach (var existing in viewModel.ExistingScreenshots)
        {
            if (viewModel.RemoveScreenshotIds.Contains(existing.Id))
            {
                continue;
            }

            screenshots.Add(new GameScreenshot
            {
                ImageSmallUrl = existing.ImageSmallUrl,
                ImageMediumUrl = existing.ImageMediumUrl,
                ImageLargeUrl = existing.ImageLargeUrl,
                DisplayOrder = existing.DisplayOrder
            });
        }

        var screenshotFiles = new[] { viewModel.Screenshot1, viewModel.Screenshot2, viewModel.Screenshot3 };
        var nextDisplayOrder = screenshots.Count > 0 ? screenshots.Max(s => s.DisplayOrder) + 1 : 1;

        foreach (var file in screenshotFiles)
        {
            if (file is not null && file.Length > 0)
            {
                var screenshotSizes = await imageService.UploadImageAsync(file, ImageType.Games);
                if (screenshotSizes is not null)
                {
                    screenshots.Add(new GameScreenshot
                    {
                        ImageSmallUrl = screenshotSizes.SmallUrl,
                        ImageMediumUrl = screenshotSizes.MediumUrl,
                        ImageLargeUrl = screenshotSizes.LargeUrl,
                        DisplayOrder = nextDisplayOrder++
                    });
                }
            }
        }

        return new Game
        {
            Id = viewModel.Id ?? 0,
            Title = viewModel.Title,
            Image = legacyImage ?? string.Empty,
            ImageSmallUrl = smallUrl,
            ImageMediumUrl = mediumUrl,
            ImageLargeUrl = largeUrl,
            ReleaseYear = viewModel.ReleaseYear,
            DeveloperPublisher = viewModel.DeveloperPublisher,
            Platforms = viewModel.Platforms,
            GenreGameplayType = viewModel.GenreGameplayType,
            FromCollective = viewModel.FromCollective,
            Description = viewModel.Description,
            YouTubeTrailerUrl = viewModel.YouTubeTrailerUrl,
            Screenshots = screenshots
        };
    }
}
