using System.ComponentModel.DataAnnotations;
using Night.Models;

namespace Night.ViewModels;

public class CollectiveMemberFormViewModel
{
    public int? Id { get; set; }

    [Required]
    [StringLength(120)]
    public string Name { get; set; } = string.Empty;

    // Legacy field (kept for backward compatibility, but ImageFile/ImageUrl preferred)
    [StringLength(240)]
    public string Image { get; set; } = string.Empty;

    // New mobile-first image fields
    public IFormFile? ImageFile { get; set; }
    public string? ImageUrl { get; set; }

    [StringLength(160)]
    public string Position { get; set; } = string.Empty;

    [StringLength(600)]
    public string Quote { get; set; } = string.Empty;

    [Required]
    public MembershipType MembershipType { get; set; } = MembershipType.Full;

    // For Subscribed members - their featured game
    public int? FeaturedGameId { get; set; }

    public List<int> SelectedGameIds { get; set; } = [];

    public IReadOnlyCollection<GameSelectViewModel> AvailableGames { get; set; } = [];

    public List<MemberGameContributionFormViewModel> GameContributions { get; set; } = new();
}
