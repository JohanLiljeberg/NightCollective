using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

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

    // Either provide an image file upload or an ImageUrl. Both are optional but one should be supplied when creating an event.
    [StringLength(240)]
    [Display(Name = "Image URL or path")]
    public string ImageUrl { get; init; } = string.Empty;

    // Optional file upload for event image. The form will send multipart/form-data when a file is included.
    public IFormFile? ImageFile { get; init; }
}
