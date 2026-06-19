using DeliveryOrderEntity = DeliveryOrder.Api.Domain.Models.DeliveryOrder;

namespace DeliveryOrder.Api.Features.Orders.Contracts;

public sealed record OrderResponse(long Id, string OrderNumber, string SenderCity, string SenderAddress,
                                   string RecipientCity, string RecipientAddress, decimal CargoWeightKg, 
                                   DateOnly PickupDate, string Status, DateTime CreatedAtUtc)
{
    public static OrderResponse FromEntity(DeliveryOrderEntity order) =>
        new(order.Id, order.OrderNumber, order.SenderCity, order.SenderAddress,
            order.RecipientCity, order.RecipientAddress, order.CargoWeightKg,
            order.PickupDate, order.Status.ToString(), order.CreatedAtUtc);
}
