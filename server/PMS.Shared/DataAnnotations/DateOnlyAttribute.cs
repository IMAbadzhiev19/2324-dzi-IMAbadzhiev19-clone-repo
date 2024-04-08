using System.ComponentModel.DataAnnotations;

namespace PMS.Shared.DataAnnotations;

/// <summary>
/// Validates date only strings with format "yyyy-MM-dd".
/// </summary>
public class DateOnlyAttribute : ValidationAttribute
{
    /// <summary>
    /// Returns whether the date only string is valid date with the format "yyyy-MM-dd".
    /// </summary>
    /// <param name="value">The object to validate.</param>
    /// <returns>True if the object is null or a valid date only string. Otherwise, false.</returns>
    public override bool IsValid(object? value)
    {
        if (value is null)
        {
            return true;
        }

        if (value is not string stringVal)
        {
            return false;
        }

        return DateOnly.TryParseExact(stringVal, "yyyy-MM-dd", out _);
    }
}