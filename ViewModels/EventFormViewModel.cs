using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Night.Validators;

namespace Night.ViewModels;

public class EventFormViewModel
{
    public int Id { get; init; }

    [StringLength(120)]
    public string Title { get; init; } = string.Empty;

    [DataType(DataType.Date)]
    [FutureDate]
    public DateTime Date { get; init; } = DateTime.Today;

    [StringLength(160)]
    public string Location { get; init; } = string.Empty;

    [StringLength(600)]
    [DataType(DataType.MultilineText)]
    public string Description { get; init; } = string.Empty;

    // Either file upload or URL (validated in controller, not via attribute)
    [Display(Name = "Image URL or path")]
    public string? ImageUrl { get; init; }

    [Display(Name = "Upload image file")]
    public IFormFile? ImageFile { get; init; }
}
