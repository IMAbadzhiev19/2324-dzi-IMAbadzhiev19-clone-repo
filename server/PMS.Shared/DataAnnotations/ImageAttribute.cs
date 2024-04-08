using System.ComponentModel.DataAnnotations;
using ImageMagick;
using Microsoft.AspNetCore.Http;

namespace PMS.Shared.DataAnnotations;

/// <summary>
/// Image attribute.
/// </summary>
public class ImageAttribute : ValidationAttribute
{
    /// <summary>
    /// Checks if the file which is uploaded is a valid image.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public override bool IsValid(object? value)
    {
        if (value == null)
        {
            return true;
        }

        if (value is not IFormFile image)
        {
            return false;
        }

        try
        {
            using var magickImage = new MagickImage(image.OpenReadStream());
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}