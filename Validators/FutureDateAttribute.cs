using System.ComponentModel.DataAnnotations;

namespace Night.Validators;

public class FutureDateAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is not DateTime dateValue)
            return false;

        return dateValue.Date >= DateTime.Today;
    }

    public override string FormatErrorMessage(string name)
    {
        return "Event date must be today or in the future.";
    }
}
