using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PMS.Data;
using PMS.Data.Models.PharmacyEntities;
using PMS.Services.Contracts.Pharmacy;
using PMS.Services.Implementations.Pharmacy;
using PMS.Services.Mapping;
using PMS.Shared.Contracts;
using PMS.Tests.Fakes;

namespace PMS.Tests;

public class NotificationServiceTests
{
    private readonly ApplicationDbContext context;
    private readonly IMapper mapper;
    private readonly ICurrentUser currentUser;
    private readonly INotificationService notificationService;

    public NotificationServiceTests()
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

        this.notificationService = new NotificationService(this.context, this.mapper);
    }

    [Fact]
    public async Task GetAssignRequestsAsync_ShouldSucceed()
    {
        var depot = new Depot
        {
            Id = "1",
            Name = "test",
            ManagerId = this.currentUser.UserId,
        };

        await this.context.Depots.AddAsync(depot);

        var notification = new Notification
        {
            Id = Guid.NewGuid().ToString(),
            IsAssignRequest = true,
            Text = "www",
            SentOn = DateOnly.FromDateTime(DateTime.Now),
            DepotId = depot.Id,
        };

        await this.context.Notifications.AddAsync(notification);
        await this.context.SaveChangesAsync();

        var notifications = await this.notificationService.GetAssignRequestsAsync("1");

        Assert.True(notifications.Count == 1);
    }

    [Fact]
    public async Task GetWarningsAsync_ShouldSucceed()
    {
        var pharmacy = new Pharmacy
        {
            Id = "1",
            Name = "t",
            Description = "t",
            FounderId = this.currentUser.UserId,
        };

        await this.context.AddAsync(pharmacy);

        var notification = new Notification
        {
            Id = Guid.NewGuid().ToString(),
            IsWarning = true,
            Text = "www",
            SentOn = DateOnly.FromDateTime(DateTime.Now),
            PharmacyId = pharmacy.Id,
        };

        await this.context.Notifications.AddAsync(notification);
        await this.context.SaveChangesAsync();

        var notifications = await this.notificationService.GetWarningsAsync("1", Services.Enums.BuildingType.Pharmacy);

        Assert.True(notifications.Count == 1);
    }

    [Fact]
    public async Task GetNotificationsForDepotAsync_ShouldSucceed()
    {
        var depot = new Depot
        {
            Id = "2",
            Name = "test",
            ManagerId = this.currentUser.UserId,
        };

        await this.context.Depots.AddAsync(depot);

        var notification = new Notification
        {
            Id = "1",
            Text = "t",
            SentOn = DateOnly.FromDateTime(DateTime.Now),
            DepotId = "2",
        };

        await this.context.Notifications.AddAsync(notification);
        await this.context.SaveChangesAsync();

        var notifications = await this.notificationService.GetNotificationsForDepotAsync("2");

        Assert.True(notifications.Count == 1);
    }

    [Fact]
    public async Task GetNotificationsForPharmacyAsync_ShouldSucceed()
    {
        var pharmacy = new Pharmacy
        {
            Id = "2",
            Name = "test",
            FounderId = this.currentUser.UserId,
        };

        await this.context.Pharmacies.AddAsync(pharmacy);

        var notification = new Notification
        {
            Id = "1",
            Text = "t",
            SentOn = DateOnly.FromDateTime(DateTime.Now),
            PharmacyId = "2",
        };

        await this.context.Notifications.AddAsync(notification);
        await this.context.SaveChangesAsync();

        var notifications = await this.notificationService.GetNotificationsForPharmacyAsync("2");

        Assert.True(notifications.Count == 1);
    }
}