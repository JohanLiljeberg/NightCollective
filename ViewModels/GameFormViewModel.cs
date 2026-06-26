using System.ComponentModel.DataAnnotations;
using Night.Models;

namespace Night.ViewModels;

public class GameFormViewModel
{
    public int? Id { get; init; }

    [Required, StringLength(120)]
    public string Title { get; init; } = string.Empty;

    [Required, StringLength(240), Display(Name = "Image URL")]
    public string Image { get; init; } = string.Empty;

    [Range(1970, 2100), Display(Name = "Release year")]
    public int ReleaseYear { get; init; } = DateTime.UtcNow.Year;

    [Required, StringLength(160), Display(Name = "Developer / Publisher")]
    public string DeveloperPublisher { get; init; } = string.Empty;

    public Platforms Platforms { get; init; } = Platforms.PC;

    [Display(Name = "Genre / Gameplay")]
    public genreGameplayType GenreGameplayType { get; init; } = genreGameplayType.Action;

    [Display(Name = "Made by the collective")]
    public bool FromCollective { get; init; } = true;

    [Required, Display(Name = "Collective member")]
    public int CollectiveMemberId { get; init; }
}
