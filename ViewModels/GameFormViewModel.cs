using System.ComponentModel.DataAnnotations;
using Night.Models;

namespace Night.ViewModels;

public class GameFormViewModel
{
    public int? Id { get; set; }

    [Required]
    [StringLength(120)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(240)]
    public string Image { get; set; } = string.Empty;

    [Range(1970, 2100)]
    public int ReleaseYear { get; set; } = DateTime.Today.Year;

    [Required]
    [StringLength(160)]
    public string DeveloperPublisher { get; set; } = string.Empty;

    public Platforms Platforms { get; set; } = Platforms.PC;

    public genreGameplayType GenreGameplayType { get; set; } = genreGameplayType.Action;

    public bool FromCollective { get; set; } = true;
}
