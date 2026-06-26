using System.ComponentModel.DataAnnotations;

namespace Night.ViewModels;

public class CollectiveMemberFormViewModel
{
    [Required]
    [StringLength(120)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(240)]
    public string Image { get; set; } = string.Empty;

    [Required]
    [StringLength(160)]
    public string Position { get; set; } = string.Empty;

    [Required]
    [StringLength(600)]
    public string Quote { get; set; } = string.Empty;

    public List<int> SelectedGameIds { get; set; } = [];

    public IReadOnlyCollection<GameSelectViewModel> AvailableGames { get; set; } = [];
}
