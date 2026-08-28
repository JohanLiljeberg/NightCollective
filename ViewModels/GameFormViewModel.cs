using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Night.Models;

namespace Night.ViewModels;

public class GameFormViewModel
{
    public int? Id { get; set; }

    [Required]
    [StringLength(120)]
    public string Title { get; set; } = string.Empty;

    // Legacy field (kept for backward compatibility, but ImageFile/ImageUrl preferred)
    [StringLength(240)]
    public string Image { get; set; } = string.Empty;

    // New mobile-first image fields
    public IFormFile? ImageFile { get; set; }
    public string? ImageUrl { get; set; }

    // Existing image (used to preview/preserve the current image while editing)
    public string? ExistingImageSmallUrl { get; set; }
    public string? ExistingImageMediumUrl { get; set; }
    public string? ExistingImageLargeUrl { get; set; }
    public bool RemoveImage { get; set; }

    [Range(1970, 2100)]
    public int ReleaseYear { get; set; } = DateTime.Today.Year;

    // Admin-controlled release status/visual indicator
    public bool IsReleased { get; set; } = true;

    // Optional specific release date, used to display a "coming soon" date for unreleased games
    public DateTime? ReleaseDate { get; set; }

    [Required]
    [StringLength(160)]
    public string DeveloperPublisher { get; set; } = string.Empty;

    public Platforms Platforms { get; set; } = Platforms.PC;

    public genreGameplayType GenreGameplayType { get; set; } = genreGameplayType.Action;

    public bool FromCollective { get; set; } = true;

    // Hide from public pages without deleting
    public bool IsHidden { get; set; }

    [StringLength(2000)]
    public string Description { get; set; } = string.Empty;

    [StringLength(500)]
    public string? YouTubeTrailerUrl { get; set; }

    // Screenshot uploads (max 3 images)
    public IFormFile? Screenshot1 { get; set; }
    public IFormFile? Screenshot2 { get; set; }
    public IFormFile? Screenshot3 { get; set; }

    // Existing screenshots (used to preview/preserve current screenshots while editing)
    public List<GameScreenshotViewModel> ExistingScreenshots { get; set; } = new();
    public List<int> RemoveScreenshotIds { get; set; } = new();

    public List<int> SelectedMemberIds { get; set; } = [];

    public IReadOnlyCollection<MemberSelectViewModel> AvailableMembers { get; set; } = [];

    public List<GameMemberContributionFormViewModel> MemberContributions { get; set; } = new();
}
