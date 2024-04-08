using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PMS.Data;
using PMS.Data.Models.PharmacyEntities;
using PMS.Services.Contracts.Pharmacy;
using PMS.Services.Implementations.Pharmacy;
using PMS.Services.Mapping;
using PMS.Shared.Contracts;
using PMS.Shared.Models.Depots;
using PMS.Tests.Fakes;

namespace PMS.Tests;

public class DepotServiceTests
{
    private readonly ApplicationDbContext context;
    private readonly IMapper mapper;
    private readonly IDepotService depotService;
    private readonly ICurrentUser currentUser;

    public DepotServiceTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        this.currentUser = new FakeCurrentUser();

        this.context = new ApplicationDbContext(options, this.currentUser);

        this.mapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        }).CreateMapper();

        this.depotService = new DepotService(this.context, this.mapper);
    }

    [Fact]
    public async Task CreateDepotAsync_WithValidData_ShouldSucceed()
    {
        var depot = new DepotIM();

        _ = await this.depotService.CreateDepotAsync(depot, this.currentUser.UserId);

        Assert.True(this.context.Depots.ToList().Count == 1);
    }

    [Fact]
    public async Task UpdateDepotAsync_WithValidData_ShouldSucceed()
    {
        var depot = new Depot() { Id = "1", Name = "Title" };
        this.context.Add(depot);
        this.context.SaveChanges();

        var newDepot = new DepotUM() { Id = "1", Name = "UpdatedTitle" };
        await this.depotService.UpdateDepotAsync(newDepot);

        Assert.NotNull(this.context.Depots.FirstOrDefault(x => x.Name == newDepot.Name));
    }

    [Fact]
    public async Task DeleteDepotAsync_ShouldSucceed()
    {
        var depot = new Depot() { Id = "1", Name = "Title" };
        this.context.Add(depot);
        this.context.SaveChanges();

        await this.depotService.DeleteDepotAsync("1");

        Assert.True(this.context.Depots.ToList().Count == 0);
    }

    [Fact]
    public async Task GetDepotById_WithValidId_ShouldReturnDepot()
    {
        var depot = new Depot() { Id = "1", Name = "Title" };
        this.context.Add(depot);
        this.context.SaveChanges();

        var result = await this.depotService.GetDepotByIdAsync("1");
        Assert.Equal(result.Name, depot.Name);
    }

    [Fact]
    public async Task GetDepots_ShouldReturnDepots()
    {
        var depot = new Depot() { Id = "1", Name = "Title" };
        var depot1 = new Depot() { Id = "2", Name = "Title" };

        this.context.AddRange(new Depot[] { depot, depot1 });
        this.context.SaveChanges();

        var depots = await this.depotService.GetAllDepotsAsync();
        Assert.True(depots.ToList().Count == 2);
    }
}