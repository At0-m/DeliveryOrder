using DeliveryOrder.Api.Features.Orders.Contracts;

namespace DeliveryOrder.Api.Features.Orders;

public sealed record CreateOrderResult(OrderResponse? Order, IReadOnlyDictionary<string, string[]> Errors)
{
    public bool IsSuccess => Order is not null;

    public static CreateOrderResult Success(OrderResponse order) => new(order, new Dictionary<string, string[]>());

    public static CreateOrderResult Failure(IReadOnlyDictionary<string, string[]> errors) => new(null, errors);
}
