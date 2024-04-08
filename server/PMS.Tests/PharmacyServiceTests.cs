using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PMS.Data;
using PMS.Data.Models.Auth;
using PMS.Data.Models.PharmacyEntities;
using PMS.Services.Contracts.Pharmacy;
using PMS.Services.Implementations.Pharmacy;
using PMS.Services.Mapping;
using PMS.Shared.Contracts;
using PMS.Shared.Models.Pharmacies;
using PMS.Tests.Fakes;

namespace PMS.Tests;

public class PharmacyServiceTests
{
    private readonly ApplicationDbContext context;
    private readonly IMapper mapper;
    private readonly IPharmacyService pharmacyService;
    private readonly ICurrentUser currentUser;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly RoleManager<IdentityRole> roleManager;

    public PharmacyServiceTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        this.currentUser = new FakeCurrentUser();
        this.context = new ApplicationDbContext(options, this.currentUser);

        this.userManager = new FakeUserManager();
        this.roleManager = new FakeRoleManager();

        this.mapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        }).CreateMapper();

        this.pharmacyService = new PharmacyService(this.context, this.mapper, this.userManager, this.roleManager);
    }

    [Fact]
    public async Task CreatePharmacyAsync_WithValdData_ShouldSucceed()
    {
        var pharmacy = new PharmacyIM
        {
            DepotId = "1",
        };

        await this.pharmacyService.CreatePharmacyAsync(pharmacy, this.currentUser.UserId);

        Assert.True(this.context.Pharmacies.ToList().Count == 1);
    }

    [Fact]
    public async Task UpdatePharmacyAsync_WidthValidData_ShouldSucceed()
    {
        var pharmacy = new Pharmacy
        {
            Id = "2",
            Name = "Test",
            DepotId = "1",
            FounderId = this.currentUser.UserId,
        };

        await this.context.Pharmacies.AddAsync(pharmacy);
        await this.context.SaveChangesAsync();

        var pharmacyUM = new PharmacyUM
        {
            Id = "2",
            Name = "Q",
        };

        await this.pharmacyService.UpdatePharmacyAsync(pharmacyUM);

        Assert.True(this.context.Pharmacies.Where(p => p.Name == "Q" && p.Id == "2").ToList().Count == 1);
    }

    [Fact]
    public async Task DeletePharmacyAsync_WidthValidId_ShouldSucceed()
    {
        var pharmacy = new Pharmacy
        {
            Id = "2",
            Name = "Test",
            DepotId = "1",
            FounderId = this.currentUser.UserId,
        };

        await this.context.Pharmacies.AddAsync(pharmacy);
        await this.context.SaveChangesAsync();

        await this.pharmacyService.DeletePharmacyAsync("2");

        Assert.True(this.context.Pharmacies.ToList().Count == 0);
    }
}