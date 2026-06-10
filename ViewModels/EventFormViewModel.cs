using System.ComponentModel.DataAnnotations;

namespace Night.ViewModels;

public class EventFormViewModel
{
    public int Id { get; init; }

    [Required]
    [StringLength(120)]
    public string Title { get; init; } = string.Empty;

    [Required]
    [DataType(DataType.Date)]
    public DateTime Date { get; init; } = DateTime.Today;

    [Required]
    [StringLength(160)]
    public string Location { get; init; } = string.Empty;

    [Required]
    [StringLength(600)]
    [DataType(DataType.MultilineText)]
    public string Description { get; init; } = string.Empty;

    [Required]
    [StringLength(240)]
    [Display(Name = "Image URL or path")]
    public string ImageUrl { get; init; } = string.Empty;
}
