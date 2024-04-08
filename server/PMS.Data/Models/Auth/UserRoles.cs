namespace PMS.Data.Models.Auth;

/// <summary>
/// Represents predefined user roles within the pharmacy system.
/// </summary>
public static class UserRoles
{
    /// <summary>
    /// Represents the founder role. The founder of the pharmacy is also the manager of it.
    /// </summary>
    public const string Founder = "Founder";

    /// <summary>
    /// Represents the pharmacist role. The pharmacist is responsible for selling medicines and providing consultation to people.
    /// </summary>
    public const string Pharmacist = "Pharmacist";

    /// <summary>
    /// Represents the depot manager role. The depot manager oversees the main depot from which the chain of pharmacies receives medicines.
    /// </summary>
    public const string DepotManager = "DepotManager";

    /// <summary>
    /// Represents the base user role. This role is assigned to everyone when creating an account.
    /// </summary>
    public const string BaseUser = "BaseUser";
}