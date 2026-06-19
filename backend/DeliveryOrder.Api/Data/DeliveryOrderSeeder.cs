using DeliveryOrder.Api.Domain.Enums;
using DeliveryOrderEntity = DeliveryOrder.Api.Domain.Models.DeliveryOrder;
using Microsoft.EntityFrameworkCore;

namespace DeliveryOrder.Api.Data;

public static class DeliveryOrderSeeder
{
    public static async Task SeedAsync(DeliveryOrderDbContext dbContext, CancellationToken cancellationToken = default)
    {
        if (await dbContext.DeliveryOrders.AnyAsync(cancellationToken))
        {
            return;
        }

        var now = DateTime.UtcNow;
        var tomorrow = DateOnly.FromDateTime(now.AddDays(1));
        var nextWeek = DateOnly.FromDateTime(now.AddDays(7));

        var orders = new[]
        {
            new DeliveryOrderEntity
            {
                OrderNumber = $"DO-{now:yyyyMMdd}-00001",
                SenderCity = "Saint Petersburg",
                SenderAddress = "Nevsky Prospect 10",
                RecipientCity = "Moscow",
                RecipientAddress = "Tverskaya Street 5",
                CargoWeightKg = 12.5m,
                PickupDate = tomorrow,
                Status = DeliveryOrderStatus.New,
                CreatedAtUtc = now.AddMinutes(-35)
            },
            new DeliveryOrderEntity
            {
                OrderNumber = $"DO-{now:yyyyMMdd}-00002",
                SenderCity = "Kazan",
                SenderAddress = "Some Street 19",
                RecipientCity = "Saint Petersburg",
                RecipientAddress = "Nevsky Avenue 71",
                CargoWeightKg = 4.2m,
                PickupDate = nextWeek,
                Status = DeliveryOrderStatus.New,
                CreatedAtUtc = now.AddMinutes(-12)
            }
        };

        dbContext.DeliveryOrders.AddRange(orders);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
