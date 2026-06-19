using DeliveryOrder.Api.Features.Orders.Contracts;

namespace DeliveryOrder.Api.Features.Orders;

public interface IDeliveryOrderService
{
    Task<IReadOnlyList<OrderResponse>> GetAllAsync(CancellationToken cancellationToken);

    Task<OrderResponse?> GetByIdAsync(long id, CancellationToken cancellationToken);

    Task<CreateOrderResult> CreateAsync(CreateOrderRequest request, CancellationToken cancellationToken);
}
