namespace PMS.Shared.Models.Medicines;

/// <summary>
/// Refill input model.
/// </summary>
public class RefillIM
{
    /// <summary>
    /// The ID of the pharmacy.
    /// </summary>
    public string PharmacyId { get; set; }

    /// <summary>
    /// The quantity to be refilled.
    /// </summary>
    public int Quantity { get; set; }
}