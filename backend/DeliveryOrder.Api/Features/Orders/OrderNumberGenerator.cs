namespace DeliveryOrder.Api.Features.Orders;

public sealed class OrderNumberGenerator : IOrderNumberGenerator
{
    public string Generate(DateTime createdAtUtc, long id) => $"DO-{createdAtUtc:yyyyMMdd}-{id:D5}";
}
