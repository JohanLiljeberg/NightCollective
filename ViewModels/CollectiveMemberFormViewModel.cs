using System.ComponentModel.DataAnnotations;

namespace Night.ViewModels;

public class CollectiveMemberFormViewModel
{
    public int? Id { get; init; }

    [Required, StringLength(120)]
    public string Name { get; init; } = string.Empty;

    [Required, StringLength(240), Display(Name = "Image URL")]
    public string Image { get; init; } = string.Empty;

    [Required, StringLength(160)]
    public string Position { get; init; } = string.Empty;

    [Required, StringLength(600)]
    public string Quote { get; init; } = string.Empty;
}
