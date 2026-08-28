using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Night.ViewModels;

public class BlogPostFormViewModel
{
    public int Id { get; init; }

    [Required]
    [StringLength(200)]
    [Display(Name = "Title")]
    public string Title { get; init; } = string.Empty;

    [StringLength(100)]
    [Display(Name = "Author")]
    public string? Author { get; init; }

    [StringLength(300)]
    [Display(Name = "Summary/Excerpt")]
    public string? Summary { get; init; }

    [Required]
    [StringLength(5000)]
    [DataType(DataType.MultilineText)]
    [Display(Name = "Story/Content")]
    public string Content { get; init; } = string.Empty;

    [StringLength(200)]
    [Display(Name = "Tags (comma-separated)")]
    public string? Tags { get; init; }

    [StringLength(500)]
    [DataType(DataType.Url)]
    [Display(Name = "External Link (optional)")]
    public string? ExternalLink { get; init; }

    [Display(Name = "Image URL or path")]
    public string? ImageUrl { get; init; }

    [Display(Name = "Upload image file")]
    public IFormFile? ImageFile { get; init; }

    [Display(Name = "Published")]
    public bool IsPublished { get; init; } = true;
}
