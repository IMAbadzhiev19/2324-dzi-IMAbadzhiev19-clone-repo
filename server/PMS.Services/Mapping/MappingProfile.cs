using AutoMapper;
using PMS.Data.Models.Auth;
using PMS.Data.Models.PharmacyEntities;
using PMS.Shared.Models.Depots;
using PMS.Shared.Models.Medicines;
using PMS.Shared.Models.Notifications;
using PMS.Shared.Models.Pharmacies;
using PMS.Shared.Models.User;

namespace PMS.Services.Mapping;

/// <summary>
/// Mapping profile used for AutoMapper configuration.
/// </summary>
public class MappingProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MappingProfile"/> class.
    /// Mapping profile constructor.
    /// </summary>
    public MappingProfile()
    {
        this.CreateMap<ApplicationUser, UserVM>();
        this.CreateMap<Depot, DepotVM>();
        this.CreateMap<Pharmacy, PharmacyVM>();
        this.CreateMap<Medicine, MedicineVM>();
        this.CreateMap<Notification, NotificationVM>();
        this.CreateMap<BasicMedicine, BasicMedicineVM>();
    }
}
