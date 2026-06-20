using DeliveryOrder.Api.Data;
using DeliveryOrder.Api.Features.Orders;
using DeliveryOrder.Api.Features.Orders.Contracts;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DeliveryOrder.Tests;

public sealed class DeliveryOrderServiceTests
{
    [Fact]
    public async Task CreateAsync_CreatesOrderAndGeneratesNumber()
    {
        await using var dbContext = CreateDbContext();
        var service = CreateService(dbContext);
        var request = CreateValidRequest();

        var result = await service.CreateAsync(request, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Order);
        Assert.Equal("DO-20260618-00001", result.Order.OrderNumber);
        Assert.Equal(1, await dbContext.DeliveryOrders.CountAsync());
    }

    [Fact]
    public async Task CreateAsync_TrimsTextFields()
    {
        await using var dbContext = CreateDbContext();
        var service = CreateService(dbContext);
        var request = CreateValidRequest() with { SenderCity = "  Saint Petersburg  " };

        var result = await service.CreateAsync(request, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal("Saint Petersburg", result.Order!.SenderCity);
    }

    [Fact]
    public async Task CreateAsync_DoesNotCreateOrder_WhenRequestIsInvalid()
    {
        await using var dbContext = CreateDbContext();
        var service = CreateService(dbContext);
        var request = CreateValidRequest() with { CargoWeightKg = -1 };

        var result = await service.CreateAsync(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
        Assert.Equal(0, await dbContext.DeliveryOrders.CountAsync());
    }

    private static DeliveryOrderService CreateService(DeliveryOrderDbContext dbContext) =>
        new(dbContext, new CreateOrderRequestValidator(), new OrderNumberGenerator(), new FixedTimeProvider(new DateTimeOffset(2026, 6, 18, 10, 0, 0, TimeSpan.Zero)));

    private static DeliveryOrderDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<DeliveryOrderDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

        return new DeliveryOrderDbContext(options);
    }

    private static CreateOrderRequest CreateValidRequest() =>
        new("Saint Petersburg", "Nevsky Prospect 10", "Moscow", "Tverskaya Street 5", 10.5m, new DateOnly(2026, 6, 19));
}
