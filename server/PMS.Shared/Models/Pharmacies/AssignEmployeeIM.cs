namespace PMS.Shared.Models.Pharmacies;

/// <summary>
/// Assign employee input model.
/// </summary>
public class AssignEmployeeIM
{
    /// <summary>
    /// The ID of the employee.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// The password of the employee.
    /// </summary>
    public string Password { get; set; } = string.Empty;
}
