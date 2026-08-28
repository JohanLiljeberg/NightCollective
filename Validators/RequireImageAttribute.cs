using System.ComponentModel.DataAnnotations;
using Night.ViewModels;

namespace Night.Validators;

/// <summary>
/// Validates that at least one image source is provided: either ImageFile or ImageUrl.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class RequireImageAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is not EventFormViewModel model)
            return false;
   
        bool hasFile = model.ImageFile is not null && model.ImageFile.Length > 0;
        bool hasUrl = !string.IsNullOrWhiteSpace(model.ImageUrl);

        bool isValid = hasFile || hasUrl;
        return isValid;
    }

    public override string FormatErrorMessage(string name)
    {
        return "Please provide an image by uploading a file or entering a URL.";
    }
}
