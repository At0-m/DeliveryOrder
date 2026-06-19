using DeliveryOrder.Api.Domain.Enums;

namespace DeliveryOrder.Api.Domain.Models;

public sealed class DeliveryOrder
{
    public long Id { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public string SenderCity { get; set; } = string.Empty;
    public string SenderAddress { get; set; } = string.Empty;
    public string RecipientCity { get; set; } = string.Empty;
    public string RecipientAddress { get; set; } = string.Empty;
    public decimal CargoWeightKg { get; set; }
    public DateOnly PickupDate { get; set; }
    public DeliveryOrderStatus Status { get; set; } = DeliveryOrderStatus.New;
    public DateTime CreatedAtUtc { get; set; }
}
